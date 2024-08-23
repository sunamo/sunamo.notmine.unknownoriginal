namespace FlvExtract

    public interface IAudioWriter : IDisposable
    {
        AudioFormat AudioFormat { get; }

        void WriteChunk(byte[] chunk, uint timeStamp);
    }
}
