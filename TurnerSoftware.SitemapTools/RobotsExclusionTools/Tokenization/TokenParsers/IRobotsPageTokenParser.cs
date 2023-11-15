namespace TurnerSoftware.RobotsExclusionTools.Tokenization.TokenParsers
{
	public interface IRobotsPageTokenParser
	{
		IEnumerable<PageAccessEntry> GetPageAccessEntries(IEnumerable<Token> tokens);
	}
}
