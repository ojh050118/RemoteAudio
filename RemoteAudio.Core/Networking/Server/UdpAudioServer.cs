using NAudio.Wave;
using RemoteAudio.Core.Audio.Windows;
using RemoteAudio.Core.Utils;
using System.Net;
using System.Net.Sockets;

namespace RemoteAudio.Core.Networking.Server
{
    public class UdpAudioServer : UdpClient
    {
        private AudioCapturer audioCapturer;
        private readonly int port;

        private RTPPacketData data;
        private readonly Random random;

        public IPAddress MulticastIPAddress { get; }

        public UdpAudioServer(int port, IPAddress multicastIPAddress)
            : base(port)
        {
            Ttl = 1;
            MulticastIPAddress = multicastIPAddress;
            JoinMulticastGroup(MulticastIPAddress);

            data = new RTPPacketData();
            random = new Random();

            audioCapturer = new AudioCapturer();
            audioCapturer.OnDataReceived += onDataAvailable;
            this.port = port;
        }

        public void Start() => audioCapturer.Start();

        public void Stop() => audioCapturer.Stop();

        private void onDataAvailable(WasapiLoopbackCapture capture, WaveInEventArgs e)
        {
            sendAudio(e.Buffer);
            OnDataAvailable(capture, e);
        }

        protected virtual void OnDataAvailable(WasapiLoopbackCapture capture, WaveInEventArgs e)
        {
        }

        private void sendAudio(byte[] audioData)
        {
            data = this.SendRtpPacket(audioData, 0, data, new IPEndPoint(MulticastIPAddress, port));
            data.SequenceNumber++;
            data.Ssrc = (uint)random.NextInt64(0, uint.MaxValue);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            audioCapturer?.Dispose();
        }
    }
}
