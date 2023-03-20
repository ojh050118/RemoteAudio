using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace RemoteAudioServer.Utils
{
    public static class NetworkUtils
    {
		public const int RTP_HEADER_SIZE = 12;
		public const int RTP_VERSION = 2;
		public const int RTP_PAYLOAD_TYPE = 0;
		public const int MAX_PACKET_SIZE = 1500;

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

		public static byte[] BuildRTPPacket(int sequenceNumber, uint timestamp, int maxPacketSize = MAX_PACKET_SIZE)
		{
			var packet = new byte[maxPacketSize];

			packet[0] = (byte)(RTP_VERSION << 6 | 0x00 << 5 | 0x00 << 4 | 0x00);
    		packet[1] = (byte)(RTP_PAYLOAD_TYPE << 0);
			packet[2] = (byte)(sequenceNumber >> 8);
        	packet[3] = (byte)(sequenceNumber >> 0);
        	packet[4] = (byte)(timestamp >> 24);
        	packet[5] = (byte)(timestamp >> 16);
        	packet[6] = (byte)(timestamp >> 8);
        	packet[7] = (byte)(timestamp >> 0);
        	packet[8] = 0x12;  // Random identifier
        	packet[9] = 0x34;  // Random identifier
        	packet[10] = 0x56; // Random identifier
        	packet[11] = 0x78; // Random identifier

			return packet;
		}
    }
}
