namespace Metaproject.PackageIndex.Structures.PackageProject
{
    public static class NuGetPackageInfoFactory
    {
        private const string Author = "cdorst";
        private const string IdPrefix = "CDorst.";

        public static NuGetPackageInfo PackageInfo(string name, string description, string version)
            => new NuGetPackageInfo
            {
                Authors = Author,
                Description = description,
                GeneratePackageOnBuild = true,
                PackageId = $"{IdPrefix}{name}",
                Version = version
            };
    }
}
