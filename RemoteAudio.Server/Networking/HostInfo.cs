﻿namespace RemoteAudio.Server.Networking
{
    public class HostInfo : IEquatable<HostInfo>
    {
        public string Provider { get; set; }
        public string DeviceName { get; set; }
        public string OS { get; set; }
        public string Address { get; set; }
        public string MultiCastAddress = UdpAudioServer.MultiCastAddress;
        public string Description { get; set; }

        public bool Equals(HostInfo h)
        {
            return Provider == h.Provider && DeviceName == h.DeviceName && OS == h.OS && Address == h.Address && Description == h.Description;
        }
    }
}
