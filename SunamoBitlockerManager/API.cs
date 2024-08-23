namespace BitLockerManager

    public partial class BitLockerManager
    {
        /// <summary>
        ///     Is OS supported.
        /// </summary>
        /// <returns>
        ///     Returns <seealso cref="bool" />
        /// </returns>
        public static bool IsSupportedOS()
        {
            // only vista or higher
            return Environment.OSVersion.Platform == PlatformID.Win32NT &&
                   Environment.OSVersion.Version >= new Version(6, 0);
        }

        /// <summary>
        ///     Enumerate drives.
        /// </summary>
        /// <returns>
        ///     Returns <seealso cref="DriveInfo" />
        /// </returns>
        public static DriveInfo[] EnumDrives()
        {
            return DriveInfo.GetDrives();
        }

        public static bool IsLocked(DriveInfo drive)
        {
            if (drive == null)
            {
                throw new Exception("Drive can't be null!");
            }

            ManagementPath path = new ManagementPath
            {
                NamespacePath = "\\ROOT\\CIMV2\\Security\\MicrosoftVolumeEncryption",
                ClassName = "Win32_EncryptableVolume"
            };
            using (ManagementClass wmiClass = new ManagementClass(path))
            {
                foreach (ManagementObject vol in wmiClass.GetInstances())
                {
                    object letterObj = vol["DriveLetter"];
                    if (letterObj == null)
                    {
                        continue;
                    }

                    string letter = letterObj.ToString();
                    if (drive.Name.StartsWith(letter, StringComparison.OrdinalIgnoreCase))
                    {
                        using (ManagementBaseObject inParams = vol.GetMethodParameters("GetLockStatus"))
                        {
                            using (ManagementBaseObject outParams = vol.InvokeMethod("GetLockStatus", inParams, null))
                            {
                                uint result = (uint)outParams["returnValue"];
                                switch (result)
                                {
                                    case 0: //S_OK
                                        uint lockStatus = (uint)outParams["LockStatus"];
                                        return lockStatus == 1;
                                    default:
                                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                                            "Unknown code {{0:X}}.", result));
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        ///     Gets protection status.
        /// </summary>
        /// <param name="drive">
        ///     The drive.
        /// </param>
        /// <returns>
        ///     The <see cref="ProtectionStatus" />.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public static ProtectionStatus GetProtectionStatus(DriveInfo drive)
        {
            if (drive == null)
            {
                throw new Exception("Drive can't be null!");
            }

            ManagementPath path = new ManagementPath
            {
                NamespacePath = "\\ROOT\\CIMV2\\Security\\MicrosoftVolumeEncryption",
                ClassName = "Win32_EncryptableVolume"
            };
            using (ManagementClass wmiClass = new ManagementClass(path))
            {
                foreach (ManagementObject vol in wmiClass.GetInstances())
                {
                    object letterObj = vol["DriveLetter"];
                    if (letterObj == null)
                    {
                        continue;
                    }

                    string letter = letterObj.ToString();

                    if (drive.Name.StartsWith(letter, StringComparison.OrdinalIgnoreCase))
                    {
                        uint status = (uint)vol["ProtectionStatus"];
                        return (ProtectionStatus)status;
                    }
                }
            }

            return ProtectionStatus.Unknown;
        }

        public static bool IsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
