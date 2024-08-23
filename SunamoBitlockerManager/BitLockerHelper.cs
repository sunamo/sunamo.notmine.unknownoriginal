//using  BitLockerManager;

using BitLockerManager2 = BitLockerManager.BitLockerManager;


/// <summary>
/// dává smysl jen tady, ne ve PlatformIndependentNuGetPackages
/// musel bych vyextrahovat interface který se ovšem velmi těžko dělá protože to není v nugetu ale vše je tady
/// </summary>
public class BitLockerHelper
{
    private static DriveInfo[] _drives;

    private static readonly List<Tuple<char, BitLockerManager2>> drives = new List<Tuple<char, BitLockerManager2>>();

    public static Func<char, bool> Init()
    {
        _drives = BitLockerManager2.EnumDrives();

        foreach (DriveInfo drive in _drives)
        {
            try
            {
                if (BitLockerManager2.GetProtectionStatus(drive) == ProtectionStatus.Protected)
                {
                    drives.Add(new Tuple<char, BitLockerManager2>(drive.Name[0], new BitLockerManager2(drive)));
                }

                if (BitLockerManager2.GetProtectionStatus(drive) == ProtectionStatus.Unknown &&
                    BitLockerManager2.IsLocked(drive))
                {
                    drives.Add(new Tuple<char, BitLockerManager2>(drive.Name[0], new BitLockerManager2(drive)));
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "Access denied ")
                {
                    throw;
                }
            }
        }

        return IsFolderLockedByBitLocker;
    }

    public static bool IsFolderLockedByBitLocker(char ch)
    {
        foreach (Tuple<char, BitLockerManager2> item in drives)
        {
            if (item.Item1 == char.ToUpper(ch))
            {
                return item.Item2.IsLocked();
            }
        }

        return false;
    }
}
