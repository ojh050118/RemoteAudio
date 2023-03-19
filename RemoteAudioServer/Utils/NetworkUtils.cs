using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace RemoteAudioServer.Utils
{
    public static class NetworkUtils
    {
        public static string GetPrimaryIPv4Address()
        {
            string ipAddress = "";

            // 모든 네트워크 인터페이스를 열어서 IP 주소를 가져옴
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            // 주로 사용하는 네트워크 인터페이스를 찾음
            NetworkInterface primaryInterface = null;
            foreach (NetworkInterface ni in interfaces)
            {
                if (ni.OperationalStatus == OperationalStatus.Up && (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
                {
                    IPInterfaceProperties ipProps = ni.GetIPProperties();
                    if (ipProps.GatewayAddresses.Count > 0)
                    {
                        primaryInterface = ni;
                        break;
                    }
                }
            }

            // 주로 사용하는 네트워크 인터페이스의 IPv4 주소를 가져옴
            if (primaryInterface != null)
            {
                IPInterfaceProperties ipProps = primaryInterface.GetIPProperties();
                foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
                {
                    if (addr.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = addr.Address.ToString();
                        break;
                    }
                }
            }

            return ipAddress;
        }
    }
}
