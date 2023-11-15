namespace TurnerSoftware.RobotsExclusionTools.Tokenization.TokenParsers
{
	public interface IRobotsFileTokenParser
	{
		IEnumerable<SiteAccessEntry> GetSiteAccessEntries(IEnumerable<Token> tokens);
		IEnumerable<SitemapUrlEntry> GetSitemapUrlEntries(IEnumerable<Token> tokens);
	}
}
