namespace TurnerSoftware.RobotsExclusionTools
{
	public interface IRobotsFileParser
	{
		RobotsFile FromString(string robotsText, Uri baseUri);
		Task<RobotsFile> FromUriAsync(Uri robotsUri);
		Task<RobotsFile> FromStreamAsync(Stream stream, Uri baseUri);
	}
}
