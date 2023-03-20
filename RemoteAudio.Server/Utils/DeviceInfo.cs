using System.Management;

namespace RemoteAudio.Server.Utils
{
    public static class DeviceInfo
    {
        private const string quary = @"SELECT * FROM Win32_OperatingSystem";

        private static ManagementObject os = new ManagementObjectSearcher(quary).Get().Cast<ManagementObject>().FirstOrDefault();

        public static string DeviceName => os["CSName"].ToString();

        public static string OS => os["Caption"].ToString();
    }
}
