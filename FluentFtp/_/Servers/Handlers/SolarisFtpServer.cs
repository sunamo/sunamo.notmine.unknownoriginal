namespace FluentFTP.Servers.Handlers

    public class SolarisFtpServer : FtpBaseServer
    {

        /// <summary>
        /// Return the FtpServer enum value corresponding to your server, or Unknown if its a custom implementation.
        /// </summary>
        public override FtpServer ToEnum()
        {
            return FtpServer.SolarisFTP;
        }

        /// <summary>
        /// Return true if your server is detected by the given SYST response message.
        /// Its a fallback method if the server did not send an identifying welcome message.
        /// </summary>
        public override bool DetectBySyst(string message)
        {

            // Detect SolarisFTP server
            // SYST response: "215 UNIX Type: L8 Version: SUNOS"
            if (message.Contains("SUNOS"))
            {
                return true;
            }

            return false;
        }

    }
}
