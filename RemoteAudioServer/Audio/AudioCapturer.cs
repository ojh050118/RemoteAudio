using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteAudioServer.Audio
{
    public class AudioCapturer : IDisposable
    {
        private readonly WasapiLoopbackCapture waveIn;
        private readonly  WaveFormat format;

        public AudioCapturer()
        {
            waveIn = new WasapiLoopbackCapture();
            format = waveIn.WaveFormat;
            waveIn.DataAvailable += onDavaAvailable;
        }

        public void Start() => waveIn.StartRecording();

        public void Stop() => waveIn.StopRecording();

        private void onDavaAvailable(object sender, WaveInEventArgs e)
        {
            OnDataAvailable((WasapiLoopbackCapture)sender, e);
        }

        protected virtual void OnDataAvailable(WasapiLoopbackCapture capture, WaveInEventArgs e)
        {
        }

        public void Dispose()
        {
            waveIn.StopRecording();
            waveIn.Dispose();
        }
    }
}
