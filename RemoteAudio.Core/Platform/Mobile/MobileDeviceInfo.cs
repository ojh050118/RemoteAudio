using Xamarin.Essentials;

namespace RemoteAudio.Core.Platform.Mobile
{
    public class MobileDeviceInfo : IDeviceInfo
    {
        public string DeviceName => DeviceInfo.Name;

        public string OS => $"{DeviceInfo.Platform} {DeviceInfo.Version.Major}.{(DeviceInfo.Version.Minor == 0 ? string.Empty : DeviceInfo.Version.Minor)}";
    }
}
