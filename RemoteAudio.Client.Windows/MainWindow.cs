using RemoteAudio.Client.Networking;
using RemoteAudio.Client.Windows.Audio;
using System.Net;

namespace RemoteAudio.Client.Windows
{
    public partial class MainWindow : Form
    {
        public const int PORT = 6974;
        private IPAddress hostIP;

        private BrodcastingController brodcasting;
        private AudioListener listener;

        public MainWindow()
        {
            InitializeComponent();

            deviceInfo.Text = $"PC Á¤º¸: {Utils.DeviceInfo.DeviceName}, {Utils.DeviceInfo.OS}";
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

            brodcasting = new BrodcastingController(PORT - 1);
            brodcasting.HostInfoReceived += h =>
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
            brodcasting.Brodcast();
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