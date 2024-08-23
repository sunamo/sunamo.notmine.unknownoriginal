namespace GoogleTranslateFreeApi

	public class LanguageAttribute: Attribute
	{
static Type type = typeof(LanguageAttribute);
		public string Iso639 { get; }
		public string FullName { get; }
		public LanguageAttribute(string iso, [CallerMemberName] string fullName = "")
		{
			if (string.IsNullOrWhiteSpace(iso))
throw new ArgumentException(nameof(iso));
			Iso639 = iso;
			FullName = fullName;
		}
	}
}
