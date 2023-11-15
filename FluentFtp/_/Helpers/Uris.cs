namespace FluentFTP.Helpers {
	/// <summary>
	/// Extension methods related to FTP tasks
	/// </summary>
	public static class Uris {
		/// <summary>
		/// Ensures that the URI points to a server, and not a directory or invalid path.
		/// </summary>
		/// <param name="uri"></param>
		public static void ValidateFtpServer(this Uri uri) {
			if (string.IsNullOrEmpty(uri.PathAndQuery)) {
								ThrowEx.UriFormat("The supplied URI does not contain a valid path.");
			}

			if (uri.PathAndQuery.EndsWith("/")) {
								ThrowEx.UriFormat("The supplied URI points at a directory.");
			}
		}


	}
}
