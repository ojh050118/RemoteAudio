using RemoteAudio.Core.Audio.Windows;
using RemoteAudio.Core.Networking;
using RemoteAudio.Core.Utils;
using System.Net;

namespace RemoteAudio.Client.Windows
{
    public partial class MainWindow : Form
    {
        public const int PORT = 6974;
        private IPAddress hostIP;

        private RemoteAudioBroadcastingController brodcasting;
        private AudioListener listener;

        private HostInfo selected;

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
                var item = new HostInfoListViewItem
                {
                    Text = $"{h.DeviceName}\n{h.Description}",
                    Current = h,
                };
                item.SubItems.AddRange(new[] { string.Empty, h.Description, h.Address });

                hostListView.Items.Add(item);
            };
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (selected == null || listener != null)
                return;

            listener = new AudioListener(new IPEndPoint(IPAddress.Parse(selected.MultiCastAddress), PORT));
            listener.Start();

            connectButton.Enabled = false;
            disconnectButton.Enabled = true;
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
            listener?.Dispose();
            listener = null;

            connectButton.Enabled = true;
            disconnectButton.Enabled = false;
        }

        private class HostInfoListViewItem : ListViewItem
        {
            public HostInfo Current { get; init; }
        }

        private void hostListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            selected = (e.Item as HostInfoListViewItem).Current;
            connectButton.Enabled = true;
        }
    }
}