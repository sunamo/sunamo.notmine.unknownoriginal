namespace ImageMagickTool
{
    class JpegExif
    {
        public JpegInfo image = null;

        public JpegExif(string path) : this(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {

        }

        public JpegExif(Stream s)
        {
            image = ExifReader.ReadJpeg(s);
        }

        /// <summary>
        /// If I date changed, for example in Windows Explorer, takes new - changed
        /// </summary>
        public DateTime? GetDateTime()
        {
            
            return ParseDateTime(image.DateTime);
        }

        private DateTime? ParseDateTime(string dateTime)
        {
            DateTime? vr = null;
            try
            {
                vr = DateTime.ParseExact(dateTime, "yyyy:MM:dd hh:mm:ss", null);
            }
            catch (Exception ex)
            {
            }
            return vr;
        }

        /// <summary>
        /// If I date changed, for example in Windows Explorer, takes old - original date taken
        /// </summary>
        public DateTime? GetDateTimeOriginal()
        {
          
            return ParseDateTime(image.DateTimeOriginal);
        }
    }
}
