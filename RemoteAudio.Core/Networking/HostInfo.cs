namespace RemoteAudio.Core.Networking
{
    public class HostInfo : IEquatable<HostInfo>
    {
        public const string NAME = @"RemoteAudio";

        /// <summary>
        /// 앱 이름. 패킷이 우리 것인지 구별하기 이해 사용됩니다.
        /// </summary>
        public string Name => NAME;

        /// <summary>
        /// 자신이 서비스할 네트워크 모드.
        /// </summary>
        public ServiceMode ServiceMode { get; set; }

        /// <summary>
        /// 장치 이름.
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 현재 구동 중인 OS 이름.
        /// </summary>
        public string OS { get; set; }

        /// <summary>
        /// 자신의 IPv4 주소.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 멀티캐스트 할 주소.
        /// </summary>
        public string MultiCastAddress = "229.1.1.229";

        /// <summary>
        /// 자신의 설명.
        /// </summary>
        public string Description { get; set; }


        public bool Equals(HostInfo h)
        {
            return ServiceMode == h.ServiceMode && DeviceName == h.DeviceName && OS == h.OS && Address == h.Address && Description == h.Description;
        }
    }
}
