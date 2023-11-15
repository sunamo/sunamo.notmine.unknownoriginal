using static DevOps.Primitives.VisualStudio.Projects.Helpers.DotNetCore.DotNetCoreProjects;
using static Metaproject.PackageIndex.Structures.PackageProject.NuGetPackageInfoFactory;

namespace Metaproject.PackageIndex.Structures.PackageProject
{
    public class PackageCsproj
    {
        public PackageCsproj(string name, string description, string packageId, string version, List<NuGetReference> packageReferences, string targetFramework)
        {
            Description = description;
            Name = name;
            PackageId = packageId;
            PackageReferences = packageReferences;
            Version = version;
            TargetFramework = targetFramework;
        }

        public string Description { get; set; }
        public string Name { get; set; }
        public string PackageId { get; set; }
        public string Version { get; set; }
        public string TargetFramework { get; set; }

        public List<NuGetReference> PackageReferences { get; set; }

        public Project GetProject()
            => Create(Name,
                TargetFramework,
                PackageReferences,
                nuGetPackageInfo: PackageInfo(PackageId, Description, Version));
    }
}
