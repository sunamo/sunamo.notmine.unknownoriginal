namespace TurnerSoftware.SitemapTools
{
	public class SitemapEntry
	{
		public Uri Location { get; set; }
		public DateTime? LastModified { get; set; }
		public ChangeFrequency? ChangeFrequency { get; set; }
		public double Priority { get; set; }
        

		public SitemapEntry()
		{
			Priority = 0.5;
		}
	}
}
