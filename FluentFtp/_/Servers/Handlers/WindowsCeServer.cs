namespace FluentFTP.Servers.Handlers

    public class WindowsCeServer : FtpBaseServer
    {

        /// <summary>
        /// Return the FtpServer enum value corresponding to your server, or Unknown if its a custom implementation.
        /// </summary>
        public override FtpServer ToEnum()
        {
            return FtpServer.WindowsCE;
        }

        /// <summary>
        /// Return true if your server is detected by the given SYST response message.
        /// Its a fallback method if the server did not send an identifying welcome message.
        /// </summary>
        public override bool DetectBySyst(string message)
        {

            // Detect WindowsCE server
            // SYST type: "Windows_CE version 7.0"
            if (message.Contains("Windows_CE"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Return the default file listing parser to be used with your FTP server.
        /// </summary>
        public virtual FtpParser GetParser()
        {
            return FtpParser.Windows;
        }

    }
}
