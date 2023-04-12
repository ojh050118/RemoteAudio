using System.Net;
using System.Net.Sockets;
using NAudio.Wave;

namespace RemoteAudio.Core.Audio.Windows;

public class AudioListener : IDisposable
{
    private readonly BufferedWaveProvider bufferedWaveProvider;
    private readonly UdpClient udpClient;
    private readonly WaveOutEvent waveOut;
    private IPEndPoint endPoint;

    public AudioListener(IPEndPoint endPoint)
    {
        udpClient = new UdpClient();
        this.endPoint = endPoint;

        udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, endPoint.Port));
        udpClient.JoinMulticastGroup(endPoint.Address);
        udpClient.MulticastLoopback = true;

        waveOut = new WaveOutEvent();
        waveOut.Init(bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(8000, 16, 1)));

        udpClient.BeginReceive(ReceiveCallback, new Tuple<byte[], int>(new byte[1024], 12));
    }

    public void Dispose()
    {
        Stop();
    }

    public void Start()
    {
        udpClient.BeginReceive(ReceiveCallback, null);
        waveOut.Play();
    }

    public void Stop()
    {
        udpClient.Close();
        waveOut.Stop();
        waveOut.Dispose();
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            var receiveBytes = udpClient.EndReceive(ar, ref endPoint);
            var headerAndPayload = ar.AsyncState as Tuple<byte[], int>;
            var buffer = new byte[headerAndPayload.Item1.Length + receiveBytes.Length];

            Array.Copy(headerAndPayload.Item1, buffer, headerAndPayload.Item1.Length);
            Array.Copy(receiveBytes, 0, buffer, headerAndPayload.Item1.Length, receiveBytes.Length);

            // RTP 페이로드에 해당하는 데이터를 추출하여 재생
            var payload = new byte[receiveBytes.Length - headerAndPayload.Item2];
            Array.Copy(receiveBytes, headerAndPayload.Item2, payload, 0, payload.Length);
            bufferedWaveProvider?.AddSamples(payload, 0, payload.Length);

            udpClient.BeginReceive(ReceiveCallback, new Tuple<byte[], int>(receiveBytes, headerAndPayload.Item2));
        }
        catch
        {
        }
    }
}