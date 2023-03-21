using NAudio.Wave;

namespace RemoteAudio.Server.Audio
{
    public class AudioCapturer : IDisposable
    {
        protected readonly WasapiLoopbackCapture WaveIn;
        protected readonly WaveFormat Format;

        public Action<WasapiLoopbackCapture, WaveInEventArgs> OnDataReceived;

        public AudioCapturer()
        {
            WaveIn = new WasapiLoopbackCapture();
            Format = WaveIn.WaveFormat;
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
