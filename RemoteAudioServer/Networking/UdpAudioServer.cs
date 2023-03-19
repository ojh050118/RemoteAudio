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


        public UdpAudioServer(int port)
        {
            server = new UdpClient(port);
            server.EnableBroadcast = true;

            this.port = port;
        }

        protected override void OnDataAvailable(WasapiLoopbackCapture capture, WaveInEventArgs e)
        {
            base.OnDataAvailable(capture, e);

            server.Send(e.Buffer.ToList().GetRange(0,1500).ToArray(), 1500, new IPEndPoint(IPAddress.Broadcast, port));
        }
    }
}
