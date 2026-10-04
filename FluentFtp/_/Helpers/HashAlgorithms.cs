namespace FluentFTP.Helpers {

	/// Helper class to convert FtpHashAlgorithm
	/// </summary>
	public static class HashAlgorithms {
		private static readonly Dictionary<string, FtpHashAlgorithm> NameToEnum = new Dictionary<string, FtpHashAlgorithm> {
			{ "A-1", FtpHashAlgorithm.SHA1 },
			{ "A-256", FtpHashAlgorithm.SHA256 },
			{ "A-512", FtpHashAlgorithm.SHA512 },
			{ "MD5", FtpHashAlgorithm.MD5 },
			{ "CRC", FtpHashAlgorithm.CRC },
		};

		private static readonly Dictionary<FtpHashAlgorithm, string> EnumToName = new Dictionary<FtpHashAlgorithm, string> {
			{ FtpHashAlgorithm.SHA1, "A-1" },
			{ FtpHashAlgorithm.SHA256, "A-256" },
			{ FtpHashAlgorithm.SHA512, "A-512" },
			{ FtpHashAlgorithm.MD5, "MD5" },
			{ FtpHashAlgorithm.CRC, "CRC" },
		};

		/// <summary>
		/// Get FtpHashAlgorithm from it's string representation
		/// </summary>
		/// <param name="ftpHashAlgorithm">Name of the hash algorithm</param>
		/// <returns>The FtpHashAlgorithm</returns>
		public static FtpHashAlgorithm FromString(string ftpHashAlgorithm) {
			if (!NameToEnum.ContainsKey(ftpHashAlgorithm)) {
								ThrowEx.NotImplementedCase("Unknown hash algorithm: " + ftpHashAlgorithm);
			}

			return NameToEnum[ftpHashAlgorithm];
		}

		/// <summary>
		/// Get string representation of FtpHashAlgorithm
		/// </summary>
		/// <param name="ftpHashAlgorithm">FtpHashAlgorithm to be converted into string</param>
		/// <returns>Name of the hash algorithm</returns>
		public static string ToString(FtpHashAlgorithm ftpHashAlgorithm)
		{
			if (!EnumToName.ContainsKey(ftpHashAlgorithm))
			{
				return ftpHashAlgorithm.ToString();
			}

			return EnumToName[ftpHashAlgorithm];
		}
	}
}
