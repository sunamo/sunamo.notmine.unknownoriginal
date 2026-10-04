namespace FluentFTP

    public partial class FtpClient : IDisposable
    {
        /// <summary>
        /// Download a file from the server and write the data into the given stream asynchronously.
        /// Reads data in chunks. Retries if server disconnects midway.
        /// </summary>
        private async Task<bool> DownloadFileInternalAsync(string localPath, string remotePath, Stream outStream, long restartPosition,
            IProgress<FtpProgress> progress, CancellationToken token, FtpProgress metaProgress, long knownFileSize, bool isAppend)
        {

            Stream downStream = null;
            var disposeOutStream = false;

            try
            {
                // get file size if downloading in binary mode (in ASCII mode we read until EOF)
                long fileLen = 0;

                if (DownloadDataType == FtpDataType.Binary && progress != null)
                {
                    fileLen = knownFileSize > 0 ? knownFileSize : await GetFileSizeAsync(remotePath, -1, token);
                }

                // open the file for reading
                downStream = await OpenReadAsync(remotePath, DownloadDataType, restartPosition, fileLen > 0, token);

                // if the server has not provided a length for this file
                // we read until EOF instead of reading a specific number of bytes
                var readToEnd = fileLen <= 0;

                const int rateControlResolution = 100;
                var rateLimitBytes = DownloadRateLimit != 0 ? (long)DownloadRateLimit * 1024 : 0;
                var chunkSize = CalculateTransferChunkSize(rateLimitBytes, rateControlResolution);

                // loop till entire file downloaded
                var buffer = new byte[chunkSize];
                var offset = restartPosition;

                var transferStarted = DateTime.Now;
                var sw = new Stopwatch();

                var anyNoop = false;

                // Fix #554: ability to download zero-byte files
                if (DownloadZeroByteFiles && outStream == null && localPath != null)
                {
                    outStream = FtpFileStream.GetFileWriteStream(this, localPath, true, QuickTransferLimit, knownFileSize, isAppend, restartPosition);
                    disposeOutStream = true;
                }

                while (offset < fileLen || readToEnd)
                {
                    try
                    {
                        // read a chunk of bytes from the FTP stream
                        var readBytes = 1;
                        long limitCheckBytes = 0;
                        long bytesProcessed = 0;

                        sw.Start();
                        while ((readBytes = await downStream.ReadAsync(buffer, 0, buffer.Length, token)) > 0)
                        {

                            // Fix #552: only create outstream when first bytes downloaded
                            if (outStream == null && localPath != null)
                            {
                                outStream = FtpFileStream.GetFileWriteStream(this, localPath, true, QuickTransferLimit, knownFileSize, isAppend, restartPosition);
                                disposeOutStream = true;
                            }

                            // write chunk to output stream
                            await outStream.WriteAsync(buffer, 0, readBytes, token);
                            offset += readBytes;
                            bytesProcessed += readBytes;
                            limitCheckBytes += readBytes;

                            // send progress reports
                            if (progress != null)
                            {
                                ReportProgress(progress, fileLen, offset, bytesProcessed, DateTime.Now - transferStarted, localPath, remotePath, metaProgress);
                            }

                            // Fix #387: keep alive with NOOP as configured and needed
                            if (!m_threadSafeDataChannels)
                            {
                                anyNoop = await NoopAsync(token) || anyNoop;
                            }

                            // honor the rate limit
                            var swTime = sw.ElapsedMilliseconds;
                            if (rateLimitBytes > 0)
                            {
                                var timeShouldTake = limitCheckBytes * 1000 / rateLimitBytes;
                                if (timeShouldTake > swTime)
                                {
                                    await Task.Delay((int)(timeShouldTake - swTime), token);
                                    token.ThrowIfCancellationRequested();
                                }
                                else if (swTime > timeShouldTake + rateControlResolution)
                                {
                                    limitCheckBytes = 0;
                                    sw.Restart();
                                }
                            }
                        }

                        // if we reach here means EOF encountered
                        // stop if we are in "read until EOF" mode
                        if (readToEnd || offset == fileLen)
                        {
                            break;
                        }

                        // zero return value (with no Exception) indicates EOS; so we should fail here and attempt to resume
                        ThrowEx.IO($"Unexpected EOF for remote file {remotePath} [{offset}/{fileLen} bytes read]");
                    }
                    catch (IOException ex)
                    {

                        // resume if server disconnected midway, or throw if there is an exception doing that as well
                        var resumeResult = await ResumeDownloadAsync(remotePath, downStream, offset, ex);
                        if (resumeResult.Item1)
                        {
                            downStream = resumeResult.Item2;
                        }
                        else
                        {
                            sw.Stop();
                            ThrowEx.ExcAsArg(ex);
                        }
                    }
                    catch (TimeoutException ex)
                    {

                        // fix: attempting to download data after we reached the end of the stream
                        // often throws a timeout exception, so we silently absorb that here
                        if (offset >= fileLen)
                        {
                            break;
                        }
                        else
                        {
                            sw.Stop();
                            ThrowEx.ExcAsArg(ex);
                        }
                    }
                }

                sw.Stop();

                // disconnect FTP stream before exiting
                if (outStream != null)
                {
                    await outStream.FlushAsync(token);
                }
                downStream.Dispose();

                // Fix #552: close the filestream if it was created in this method
                if (disposeOutStream)
                {
                    outStream.Dispose();
                    disposeOutStream = false;
                }

                // send progress reports
                if (progress != null)
                {
                    progress.Report(new FtpProgress(100.0, offset, 0, TimeSpan.Zero, localPath, remotePath, metaProgress));
                }

                // FIX : if this is not added, there appears to be "stale data" on the socket
                // listen for a success/failure reply
                try
                {
                    while (!m_threadSafeDataChannels)
                    {
                        FtpReply status = await GetReplyAsync(token);

                        // Fix #387: exhaust any NOOP responses (not guaranteed during file transfers)
                        if (anyNoop && status.Message != null && status.Message.Contains("NOOP"))
                        {
                            continue;
                        }

                        // Fix #353: if server sends 550 or 5xx the transfer was received but could not be confirmed by the server
                        // Fix #509: if server sends 450 or 4xx the transfer was aborted or failed midway
                        if (status.Code != null && !status.Success)
                        {
                            return false;
                        }

                        // Fix #387: exhaust any NOOP responses also after "226 Transfer complete."
                        if (anyNoop)
                        {
                            await ReadStaleDataAsync(false, true, true, token);
                        }

                        break;
                    }
                }

                // absorb "System.TimeoutException: Timed out trying to read data from the socket stream!" at GetReply()
                catch (Exception) { }

                return true;
            }
            catch (IOException ex1)
            {
                LogStatus(FtpTraceLevel.Verbose, "IOException for file " + localPath + " : " + ex1.Message);
                return false;
            }
            catch (Exception ex1)
            {

                // close stream before throwing error
                try
                {
                    downStream.Dispose();
                }
                catch (Exception)
                {
                }

                // Fix #552: close the filestream if it was created in this method
                if (disposeOutStream)
                {
                    try
                    {
                        outStream.Dispose();
                        disposeOutStream = false;
                    }
                    catch (Exception)
                    {
                    }
                }

                if (ex1 is OperationCanceledException ex)
                {
                    LogStatus(FtpTraceLevel.Info, "Download cancellation requested");
                    ThrowEx.ExcAsArg(ex);
                }

                // absorb "file does not exist" exceptions and simply return false
                if (ex1.Message.IsKnownError(FtpServerStrings.fileNotFound))
                {
                    LogStatus(FtpTraceLevel.Error, "File does not exist: " + ex1.Message);
                    return false;
                }

                // catch errors during upload
                ThrowEx.Ftp("Error while downloading the file from the server. See InnerException for more info.", ex1);
            }

            return false;
        }
    }
}
