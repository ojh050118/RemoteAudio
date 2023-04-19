using NAudio.CoreAudioApi;
using RemoteAudio.Core.Networking;
using RemoteAudio.Core.Utils;
using RemoteAudio.Server.Windows.Networking;

namespace RemoteAudio.Server.Windows
{
    public partial class MainWindow : Form
    {
        public UdpAudioServer Server;
        public const int PORT = 6974;

        private RemoteAudioBroadcastingController brodcasting;

        private MMDevice defaultDevice;

        public MainWindow()
        {
            InitializeComponent();

            var info = PlatformUtil.GetDeviceInfo();
            deviceInfo.Text = $"PC 정보: {info.DeviceName}, {info.OS}";

            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            Server = new UdpAudioServer(PORT);

            var hostInfo = new HostInfo
            {
                ServiceMode = ServiceMode.Server,
                DeviceName = info.DeviceName,
                OS = info.OS,
                Address = NetworkUtil.GetPrimaryIPv4Address(),
                MultiCastAddress = Server.MulticastIPAddress.ToString()
            };

            brodcasting = new ServerBroadcastingController(PORT - 1, hostInfo);
        
            deviceDescription.TextChanged += (s, e) => brodcasting.HostInfo.Description = deviceDescription.Text;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var value = (int)(defaultDevice.AudioMeterInformation.MasterPeakValue * progressBar.Maximum);

            progressBar.Value = value;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Server.Start();

            startButton.Enabled = false;
            stopButton.Enabled = true;
            deviceDescription.Enabled = false;
            statusText.Text = "멀티캐스팅 중";

            brodcasting.ReceiveBroadcast();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            Server.Stop();

            startButton.Enabled = true;
            stopButton.Enabled = false;
            deviceDescription.Enabled = true;
            statusText.Text = "준비";

            brodcasting.StopReceivingBroadcast();
        }
    }
}
