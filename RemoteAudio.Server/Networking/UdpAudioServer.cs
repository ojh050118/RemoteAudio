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

        private RTPPacketData data;

        private IPAddress address;

        public const string MultiCastAddress = "229.1.1.229";

        private readonly Random random;


        public UdpAudioServer(int port)
        {
            address = IPAddress.Parse(MultiCastAddress);

            server = new UdpClient(port);
            server.JoinMulticastGroup(address);
            server.Ttl = 1;

            data = new RTPPacketData();
            random = new Random();

            this.port = port;
        }

        protected override void OnDataAvailable(WasapiLoopbackCapture capture, WaveInEventArgs e)
        {
            base.OnDataAvailable(capture, e);

            sendAudio(e.Buffer);
        }

        private void sendAudio(byte[] audioData)
        {
            data = server.SendRtpPacket(audioData, 0, data, new IPEndPoint(address, port));
            data.SequenceNumber++;
            data.Ssrc = (uint)random.NextInt64(0, uint.MaxValue);
        }
    }
}
