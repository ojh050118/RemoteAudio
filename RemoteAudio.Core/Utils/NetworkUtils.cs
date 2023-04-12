using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace RemoteAudio.Core.Utils
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

        public static byte[] CreateRtpHeader(int payloadType, RTPPacketData data)
        {
            byte[] header = new byte[12];

            // RTP Version: 2 bits
            header[0] = 0x80;

            // Payload Type: 7 bits
            header[1] = (byte)(payloadType & 0x7F);

            // Sequence Number: 16 bits
            header[2] = (byte)((data.SequenceNumber >> 8) & 0xFF);
            header[3] = (byte)(data.SequenceNumber & 0xFF);

            // Timestamp: 32 bits
            header[4] = (byte)((data.Timestamp >> 24) & 0xFF);
            header[5] = (byte)((data.Timestamp >> 16) & 0xFF);
            header[6] = (byte)((data.Timestamp >> 8) & 0xFF);
            header[7] = (byte)(data.Timestamp & 0xFF);

            // SSRC: 32 bits
            header[8] = (byte)((data.Ssrc >> 24) & 0xFF);
            header[9] = (byte)((data.Ssrc >> 16) & 0xFF);
            header[10] = (byte)((data.Ssrc >> 8) & 0xFF);
            header[11] = (byte)(data.Ssrc & 0xFF);

            return header;
        }

        public static RTPPacketData SendRtpPacket(this UdpClient udpClient, byte[] audioData, int payloadType, RTPPacketData packetData, IPEndPoint remoteEndpoint)
        {
            int dataSize = MAX_PACKET_SIZE - 12;
            int offset = 0;

            while (offset < audioData.Length)
            {
                int remaining = audioData.Length - offset;

                if (remaining < dataSize)
                    dataSize = remaining;

                byte[] packet = new byte[MAX_PACKET_SIZE];
                byte[] data = new byte[dataSize];

                Array.Copy(audioData, offset, data, 0, dataSize);

                byte[] header = CreateRtpHeader(payloadType, packetData);
                Array.Copy(header, packet, header.Length);

                Array.Copy(data, 0, packet, header.Length, data.Length);

                udpClient.Send(packet, packet.Length, remoteEndpoint);

                offset += dataSize;
                packetData.SequenceNumber++;
                packetData.Timestamp += (uint)dataSize;
            }

            return packetData;
        }

        public static IPAddress GetRandomMulticastAddress()
        {
            var random = new Random();
            var bit1 = random.Next(224, 240);
            var nextBit = () => random.Next(0, 256);

            return IPAddress.Parse($"{bit1}.{nextBit.Invoke()}.{nextBit.Invoke()}.{nextBit.Invoke()}");
        }
    }

    public struct RTPPacketData
    {
        public uint SequenceNumber;
        public uint Timestamp;
        public uint Ssrc;

        public RTPPacketData(uint sequenceNumber, uint timestamp, uint ssrc)
        {
            SequenceNumber = sequenceNumber;
            Timestamp = timestamp;
            Ssrc = ssrc;
        }
    }
}
