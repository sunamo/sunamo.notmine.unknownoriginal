using static System.String;

namespace Metaproject.PackageIndex.Functions.ParseCsprojFile

    public static class CsprojFileParser
    {
        public static PackageCsproj ParseCsproj(IEnumerable<string> lines, string path)
        {
            string packageId = string.Empty;
            return new PackageCsproj(
                GetName(path),
                GetTagValue("<Description>", lines),
                packageId,
                GetTagValue("<Version>", lines),
                GetReferences(lines).ToList(),
                GetTagValue("<TargetFramework>", lines));
        }

        public static PackageCsproj ParseCsproj(string path)
        {
            var lines = File.ReadAllLines(path)
                .Where(line => !IsNullOrWhiteSpace(line));
            return ParseCsproj(lines, path);
        }

        private static string GetName(string path)
        {
            var name = new FileInfo(path).Name;
            return name.Substring(0, name.Length - ".csproj".Length);
        }

        private static IEnumerable<NuGetReference> GetReferences(IEnumerable<string> lines)
        {
            foreach (var line in lines.Where(it
                => it.Contains("<PackageReference")
                || it.Contains("<DotNetCliToolsReference")))
            {
                var split = line.Split('"');
                yield return new NuGetReference(
                    include: split.ElementAt(2),
                    version: split.ElementAt(4)
                    //line.Contains("<PackageReference")
                    //    ? ReferenceType.PackageReference
                    //    : ReferenceType.DotNetCliToolReference
                    );
            }
        }

        private static string GetTagValue(string tag, string line)
        {
            if (IsNullOrWhiteSpace(line))
            {
                return null;
            }
            else
            {
                #region MyRegion
                //var splitted = lineSH.Split(tag);
                //var ea = splitted.ElementAt(2);
                //var ss = ea.Substring(1);
                //var re = ss.Reverse();
                //var sk = re.Skip(2);
                //var re2 = sk.Reverse();
                //var ta  = re2.ToArray();
                //var r = new string(ta);
                //return r; 
                #endregion

                line = line.Trim().TrimStart(AllChars.lt);
                string end = tag.Replace(AllStrings.lt, "</");
                string s = null;
                if (line.Contains(end))
                {
                    s = SH.GetTextBetweenTwoChars(line, AllChars.gt, AllChars.lt);
                }
                else
                {
                    var dx = line.IndexOf(AllChars.gt);
                    s = line.Substring(dx + 1);
                }


#if DEBUG
                //if (tag == "<TargetFramework>")
                //{

                //}
#endif

                return s;
            }
        }

        private static string GetTagValue(string tag, IEnumerable<string> lines)
        {
            var line = lines
                  .Where(line => line.Contains(tag))
                  .FirstOrDefault();
            return GetTagValue(tag, line);
        }
    }
}
