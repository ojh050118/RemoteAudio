namespace RemoteAudio.Core.Platform
{
    public interface IDeviceInfo
    {
        string DeviceName { get; }

        string OS { get; }
    }
}
