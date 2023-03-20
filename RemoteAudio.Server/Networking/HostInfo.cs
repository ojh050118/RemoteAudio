namespace RemoteAudio.Server.Networking
{
    public class HostInfo
    {
        public string Provider { get; set; }
        public string DeviceName { get; set; }
        public string OS { get; set; }
        public string Address { get; set; }
        public string MultiCastAddress = UdpAudioServer.MultiCastAddress;
    }
}
