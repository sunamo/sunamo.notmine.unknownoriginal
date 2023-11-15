using static Metaproject.PackageIndex.Functions.ParseCsprojFile.CsprojFileParser;
using static System.IO.Directory;

namespace Metaproject.PackageIndex.Functions.ParseCsprojFiles
{
    public static class CsprojFilesParser
    {
        public static IEnumerable<PackageCsproj> ParseCsprojs(string directory)
        {
            foreach (var file in EnumerateFiles(directory, "*.csproj", SearchOption.AllDirectories))
                yield return ParseCsproj(file);
        }
    }
}
