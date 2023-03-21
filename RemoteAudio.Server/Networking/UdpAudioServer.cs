using NAudio.Wave;
using RemoteAudio.Server.Audio;
using RemoteAudio.Server.Utils;
using System.Net;
using System.Net.Sockets;

namespace RemoteAudio.Server.Networking
{
    public class UdpAudioServer : AudioCapturer
    {
        private UdpClient server;
        private int port;

        private int sequenceNumber;
        private uint timestamp;

        private IPAddress address;

        public const string MultiCastAddress = "229.1.1.229";


        public UdpAudioServer(int port)
        {
            address = IPAddress.Parse(MultiCastAddress);

            server = new UdpClient(port);
            server.JoinMulticastGroup(address);
            server.Ttl = 1;

            this.port = port;
        }

        protected override void OnDataAvailable(WasapiLoopbackCapture capture, WaveInEventArgs e)
        {
            base.OnDataAvailable(capture, e);

            sendAudio(e.Buffer, e.BytesRecorded);
        }

        private void sendAudio(byte[] audioData, int length)
        {
            timestamp += (uint)(audioData.Length / 2);
            var packet = NetworkUtils.BuildRTPPacket(sequenceNumber, timestamp);

            Array.Copy(audioData, 0, packet, NetworkUtils.RTP_HEADER_SIZE, length);

            // Todo: 오디오 데이터를 여러개의 RTP패킷으로 나누어 전송
            server.Send(packet, NetworkUtils.RTP_HEADER_SIZE + length, new IPEndPoint(address, port));

            sequenceNumber++;
        }
    }
}
