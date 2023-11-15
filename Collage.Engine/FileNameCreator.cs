namespace Collage.Engine
{
    using SunamoExceptions;
    using System;
    using System.IO;
    public class FileNameCreator
    {
static Type type = typeof(FileNameCreator);
        public DirectoryInfo OutputDirectory { get; private set; }
        public FileNameCreator(DirectoryInfo outputDirectory)
        {
            if (outputDirectory == null)
            {
                ThrowEx.IsNull("outputDirectory");
            }
            if (!outputDirectory.Exists)
            {
                ThrowEx.Custom("Output directory does not exist");
            }
            this.OutputDirectory = outputDirectory;
        }
        public string CreateFileName()
        {
            string fileName = string.Format("collage-{0:yyyy-MM-dd_HHmm}.jpg", DateTime.Now);
            return Path.Combine(this.OutputDirectory.FullName, fileName);
        } 
    }
}