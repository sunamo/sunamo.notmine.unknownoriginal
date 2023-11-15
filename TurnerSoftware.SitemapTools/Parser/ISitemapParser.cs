namespace TurnerSoftware.SitemapTools.Parser
{
	public interface ISitemapParser
	{
		SitemapFile ParseSitemap(TextReader reader);
	}
}
