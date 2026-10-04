namespace GoogleTranslateFreeApi
{
	[DataContract]
	public class TranslationResult
	{
		[DataMember]
		public string[] FragmentedTranslation { get; set; }
		
		public string MergedTranslation => String.Concat(FragmentedTranslation);
		
		[DataMember]
		public string OriginalText { get; set; }
		[DataMember]
		public string TranslatedTextTranscription { get; set; }
		[DataMember]
		public string OriginalTextTranscription { get; set; }
		[DataMember]
		public string[] SeeAlso { get; set; }
		[DataMember]
		public Language SourceLanguage { get; set; }
		[DataMember]
		public Language TargetLanguage { get; set; }
		[DataMember]
		public Synonyms Synonyms { get; set; }
		[DataMember]
		public Corrections Corrections { get; set; }
		[DataMember]
		public Definitions Definitions { get; set; }
		[DataMember]
		public ExtraTranslations ExtraTranslations { get; set; }

		public TranslationResult() { }
	}
}
