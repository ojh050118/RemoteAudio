using NAudio.Wave;
using RemoteAudioServer.Audio;
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

        public UdpAudioServer(int port)
        {
            server = new UdpClient(port);
            address = IPAddress.Parse(NetworkUtils.GetPrimaryIPv4Address());
			server.JoinMultiCastGroup(address);
			server.Ttl = 1;

            this.port = port;
        }

        protected override void OnDataAvailable(WasapiLoopbackCapture capture, WaveInEventArgs e)
        {
            base.OnDataAvailable(capture, e);

			sendAudio(e.Buffer);
        }

		private void sendAudio(byte[] audioData)
		{
			timestamp += (uint)(audioData.Length / 2);
			var packet = NetworkUtils.BuildRTPPacket(sequenceNumber, timestamp);
			
			Array.Copy(audioData, 0, packet, NetworkUtils.RTP_HEADER_SIZE, packet.Length);

			server.Send(packet, NetworkUtils.RTP_HEADER_SIZE + audioData.Length, new IPEndPoint(address, port));
			
			sequenceNumber++;
		}
    }
}
