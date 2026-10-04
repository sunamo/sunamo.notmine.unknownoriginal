namespace CommandLine.Core

    static class TypeLookup
    {
        public static Maybe<TypeDescriptor> FindTypeDescriptorAndSibling(
            string name,
            IEnumerable<OptionSpecification> specifications,
            StringComparer comparer)
        {
            Dictionary<string, long> longNames = new Dictionary<string, long>();
            foreach (var item in specifications)
            {
                //if (item.LongName == "FormatTo")
                //{
                //    Debug.WriteLine(item.LongName);
                //}
                DictionaryHelper.AddOrPlus(longNames, item.LongName, 1);
            }

            var s2 = longNames.Where(d => d.Value > 1);

            if (s2.Count() > 0)
            {
                ThrowEx.Custom(s2.First().Key + $" is {s2.First().Value} times in *Args class");
            }

            OptionSpecification sod = null;

            sod = specifications.SingleOrDefault(a => name.MatchName(a.ShortName, a.LongName, comparer));
            var maybe = sod.ToMaybe();
            var info = maybe.Map(
                        first =>
                            {
                                var descr = TypeDescriptor.Create(first.TargetType, first.Max);
                                var next = specifications
                                    .SkipWhile(s => s.Equals(first)).Take(1)
                                    .SingleOrDefault(x => x.IsValue()).ToMaybe()
                                    .Map(second => TypeDescriptor.Create(second.TargetType, second.Max));
                                return descr.WithNextValue(next);
                            });




            return info;

        }
    }
}
