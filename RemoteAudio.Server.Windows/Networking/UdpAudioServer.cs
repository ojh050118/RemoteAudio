using NAudio.Wave;
using RemoteAudio.Core.Audio.Windows;
using RemoteAudio.Core.Utils;
using System.Net;
using System.Net.Sockets;

namespace RemoteAudio.Server.Windows.Networking
{
    public class UdpAudioServer : AudioCapturer
    {
        private UdpClient server;
        private int port;

        private RTPPacketData data;
        private readonly Random random;

        public IPAddress MulticastIPAddress { get; }

        public UdpAudioServer(int port)
        {
            MulticastIPAddress = NetworkUtil.GetRandomMulticastAddress();
            server = new UdpClient(port);
            server.JoinMulticastGroup(MulticastIPAddress);
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
            data = server.SendRtpPacket(audioData, 0, data, new IPEndPoint(MulticastIPAddress, port));
            data.SequenceNumber++;
            data.Ssrc = (uint)random.NextInt64(0, uint.MaxValue);
        }
    }
}
