namespace TurnerSoftware.RobotsExclusionTools.Tokenization
{
	public interface ITokenizer
	{
		IEnumerable<Token> Tokenize(string text);
		IEnumerable<Token> Tokenize(TextReader reader);
		Task<IEnumerable<Token>> TokenizeAsync(TextReader reader);
	}
}
