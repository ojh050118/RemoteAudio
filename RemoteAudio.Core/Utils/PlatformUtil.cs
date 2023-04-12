using RemoteAudio.Core.Platform;
using RemoteAudio.Core.Platform.Mobile;
using RemoteAudio.Core.Platform.Windows;
using System.Runtime.InteropServices;

namespace RemoteAudio.Core.Utils
{
    public static class PlatformUtil
    {
        public static IDeviceInfo GetDeviceInfo()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    return new WindowsDeviceInfo();

                case PlatformID.Unix:
                    var osInfo = RuntimeInformation.OSDescription;

                    if (osInfo.Contains("Linux"))
                        throw new NotSupportedException();

                    return new MobileDeviceInfo();

                default:
                    throw new NotSupportedException();
            }
        }

        public static T GetDeviceInfo<T>() where T : class, IDeviceInfo => GetDeviceInfo() as T;
    }
}
