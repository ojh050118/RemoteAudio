using NAudio.Wave;

namespace RemoteAudio.Server.Audio
{
    public class AudioCapturer : IDisposable
    {
        protected WasapiLoopbackCapture WaveIn { get; private set; }
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

        private WaveFormat waveFormat;

        public Action<WasapiLoopbackCapture, WaveInEventArgs> OnDataReceived;

        public AudioCapturer()
        {
            WaveIn = new WasapiLoopbackCapture();
            waveFormat = WaveIn.WaveFormat;
            WaveIn.DataAvailable += onDavaAvailable;
        }

        public void Start() => WaveIn.StartRecording();

        public void Stop() => WaveIn.StopRecording();

        private void onDavaAvailable(object sender, WaveInEventArgs e)
        {
            OnDataReceived?.Invoke(WaveIn, e);
            OnDataAvailable(WaveIn, e);
        }

        protected virtual void OnDataAvailable(WasapiLoopbackCapture capture, WaveInEventArgs e)
        {
        }

        public void Dispose()
        {
            WaveIn.StopRecording();
            WaveIn.Dispose();
        }
    }
}
