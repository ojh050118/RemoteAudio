using NAudio.Wave;
using RemoteAudioServer.Audio;
using RemoteAudioServer.Utils;
using System.Net;
using System.Net.Sockets;

namespace RemoteAudioServer.Networking
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
            server = new UdpClient(port);
            address = IPAddress.Parse(MultiCastAddress);
            server.JoinMulticastGroup(address);
            server.Ttl = 1;

            WaveIn.BufferMilliseconds = 50;

            this.port = port;
        }

        protected override void OnDataAvailable(WaveIn capture, WaveInEventArgs e)
        {
            base.OnDataAvailable(capture, e);

            sendAudio(e.Buffer, e.BytesRecorded);
        }

        private void sendAudio(byte[] audioData, int length)
        {
            timestamp += (uint)(audioData.Length / 2);
            var packet = NetworkUtils.BuildRTPPacket(sequenceNumber, timestamp);

            Array.Copy(audioData, 0, packet, NetworkUtils.RTP_HEADER_SIZE, length);

            server.Send(packet, NetworkUtils.RTP_HEADER_SIZE + length, new IPEndPoint(address, port));

            sequenceNumber++;
        }
    }
}
