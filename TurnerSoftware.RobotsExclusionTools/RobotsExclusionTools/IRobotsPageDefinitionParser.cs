namespace TurnerSoftware.RobotsExclusionTools
{
	public interface IRobotsPageDefinitionParser
	{
		RobotsPageDefinition FromRules(IEnumerable<string> rules);
	}
}
