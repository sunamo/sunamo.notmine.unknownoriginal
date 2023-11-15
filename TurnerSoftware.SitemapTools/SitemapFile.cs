namespace TurnerSoftware.SitemapTools
{
	public class SitemapFile
	{
		public Uri Location { get; set; }
		public IEnumerable<SitemapIndexEntry> Sitemaps { get; set; }
		public IEnumerable<SitemapEntry> Urls { get; set; }

		public SitemapFile()
		{
			Sitemaps = Enumerable.Empty<SitemapIndexEntry>();
			Urls = Enumerable.Empty<SitemapEntry>();
		}
	}
}
