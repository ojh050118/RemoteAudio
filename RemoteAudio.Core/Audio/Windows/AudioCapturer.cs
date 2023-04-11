using NAudio.Wave;

namespace RemoteAudio.Core.Audio.Windows;

public class AudioCapturer : IAudioCapturer, IDisposable
{
    public Action<WasapiLoopbackCapture, WaveInEventArgs> OnDataReceived;

    private WaveFormat waveFormat;

    public AudioCapturer()
    {
        WaveIn = new WasapiLoopbackCapture();
        waveFormat = WaveIn.WaveFormat;
        WaveIn.DataAvailable += onDavaAvailable;
    }

    protected WasapiLoopbackCapture WaveIn { get; }

    public WaveFormat Format
    {
        get => waveFormat;
        set
        {
            if (ReferenceEquals(waveFormat, value))
                return;

            waveFormat = value;
            WaveIn.WaveFormat = value;
        }
    }

    public void Start()
    {
        WaveIn.StartRecording();
    }

    public void Stop()
    {
        WaveIn.StopRecording();
    }

    public void Dispose()
    {
        WaveIn.StopRecording();
        WaveIn.Dispose();
    }

    private void onDavaAvailable(object sender, WaveInEventArgs e)
    {
        OnDataReceived?.Invoke(WaveIn, e);
        OnDataAvailable(WaveIn, e);
    }

    protected virtual void OnDataAvailable(WasapiLoopbackCapture capture, WaveInEventArgs e)
    {
    }
}