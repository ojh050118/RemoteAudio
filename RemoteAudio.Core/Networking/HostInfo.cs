using RemoteAudio.Core.Utils;
using System.Net;
using Xamarin.Essentials;

namespace RemoteAudio.Core.Networking
{
    public class HostInfo : IEquatable<HostInfo>
    {
        public const string NAME = @"RemoteAudio";

        /// <summary>
        /// 앱 이름. 패킷이 우리 것인지 구별하기 위해 사용됩니다.
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
        /// 멀티캐스트를 할 주소.
        /// </summary>
        public string MultiCastAddress { get; set; }

        /// <summary>
        /// 자신의 설명.
        /// </summary>
        public string Description { get; set; }

        public static HostInfo GetHostInfo(ServiceMode serviceMode)
        {
            var deviceInfo = PlatformUtil.GetDeviceInfo();

            return new HostInfo
            {
                ServiceMode = serviceMode,
                DeviceName = deviceInfo.DeviceName,
                OS = deviceInfo.OS,
                Address = NetworkUtil.GetPrimaryIPv4Address(),
                MultiCastAddress = NetworkUtil.GetRandomMulticastAddress().ToString(),
            };
        }

        public static HostInfo GetHostInfo(ServiceMode serviceMode, IPAddress multiCastAddress)
        {
            var deviceInfo = PlatformUtil.GetDeviceInfo();

            return new HostInfo
            {
                ServiceMode = serviceMode,
                DeviceName = deviceInfo.DeviceName,
                OS = deviceInfo.OS,
                Address = NetworkUtil.GetPrimaryIPv4Address(),
                MultiCastAddress = multiCastAddress.ToString(),
            };
        }


        public bool Equals(HostInfo h)
        {
            return ServiceMode == h.ServiceMode &&
                   DeviceName == h.DeviceName &&
                   OS == h.OS &&
                   Address == h.Address &&
                   Description == h.Description;
        }
    }
}
