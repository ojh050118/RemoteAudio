using NAudio.Wave;

namespace RemoteAudio.Server.Audio
{
    public class AudioCapturer : IDisposable
    {
        protected readonly WaveIn WaveIn;
        protected readonly WaveFormat Format;

        public AudioCapturer()
        {
            WaveIn = new WaveIn();
            Format = WaveIn.WaveFormat;
            WaveIn.DataAvailable += onDavaAvailable;
        }

        public void Start() => WaveIn.StartRecording();

        public void Stop() => WaveIn.StopRecording();

        private void onDavaAvailable(object sender, WaveInEventArgs e)
        {
            OnDataAvailable(WaveIn, e);
        }

        protected virtual void OnDataAvailable(WaveIn capture, WaveInEventArgs e)
        {
        }

        public void Dispose()
        {
            WaveIn.StopRecording();
            WaveIn.Dispose();
        }
    }
}
