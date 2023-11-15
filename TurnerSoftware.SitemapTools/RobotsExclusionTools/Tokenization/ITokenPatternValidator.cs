namespace TurnerSoftware.RobotsExclusionTools.Tokenization
{
	public interface ITokenPatternValidator
	{
		TokenValidationResult Validate(IEnumerable<Token> tokens);
	}
}
