using System.Management;

namespace RemoteAudio.Core.Platform.Windows
{
    public sealed class WindowsDeviceInfo : IDeviceInfo
    {
        private const string quary = @"SELECT * FROM Win32_OperatingSystem";

        private static ManagementObject os
        {
            get
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    return new ManagementObjectSearcher(quary).Get().Cast<ManagementObject>().FirstOrDefault();

                return null;
            }
        }

        public string DeviceName => os["CSName"]?.ToString();

        public string OS => os["Caption"]?.ToString();
    }
}
