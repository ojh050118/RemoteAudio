using RemoteAudio.Core.Networking;
using RemoteAudio.Core.Audio.Windows;
using System.Net;
using RemoteAudio.Core.Utils;

namespace RemoteAudio.Client.Windows
{
    public partial class MainWindow : Form
    {
        public const int PORT = 6974;
        private IPAddress hostIP;

        private RemoteAudioBroadcastingController brodcasting;
        private AudioListener listener;

        public MainWindow()
        {
            InitializeComponent();

            var info = PlatformUtils.GetDeviceInfo();
            deviceInfo.Text = $"PC Á¤º¸: {info.DeviceName}, {info.OS}";

            hostIPAddress.TextChanged += (s, e) =>
            {
                if (IPAddress.TryParse(hostIPAddress.Text, out IPAddress ipAddress))
                {
                    hostIP = ipAddress;
                    directConnectButton.Enabled = true;
                }
                else
                {
                    hostIP = null;
                    directConnectButton.Enabled = false;
                }
            };

            var hostInfo = new HostInfo
            {
                ServiceMode = ServiceMode.Client,
                DeviceName = info.DeviceName,
                OS = info.OS,
                Address = NetworkUtils.GetPrimaryIPv4Address(),
                MultiCastAddress = string.Empty
            };

            brodcasting = new RemoteAudioBroadcastingController(PORT - 1, hostInfo, ServiceMode.Server);
            brodcasting.DataReceived += h =>
            {
                hostListView.Items.Add(new ListViewItem
                {
                    Text = $"{h.DeviceName}\n{h.Description}"
                });
            };
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            notifyIcon.ShowBalloonTip(5);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            brodcasting.Broadcast();
            brodcasting.ReceiveBroadcast();
        }

        private void directConnectButton_Click(object sender, EventArgs e)
        {
            listener?.Stop();
            listener = new AudioListener(new IPEndPoint(hostIP, PORT));
            listener.Start();
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            listener?.Stop();
            listener = null;
        }
    }
}