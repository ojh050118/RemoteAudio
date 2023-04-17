using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RemoteAudio.Core.Networking;
using RemoteAudio.Core.Platform;
using RemoteAudio.Core.Utils;
using RemoteAudio.Core.Networking.Server;
using RemoteAudio.Server.Windows.Helper;

namespace RemoteAudio.Server.Windows.Pages
{
    public sealed partial class HomePage : Page
    {
        public const int PORT = 6974;

        private IDeviceInfo deviceInfo;
        private MainWindow mainWindow;
        private HostInfo hostInfo;

        private UdpAudioServer server;
        private RemoteAudioBroadcastingController broadcasting;

        public HomePage()
        {
            InitializeComponent();

            deviceInfo = PlatformUtil.GetDeviceInfo();
            Loaded += (_, __) => mainWindow = WindowHelper.GetWindowForElement(this) as MainWindow;

            server = new UdpAudioServer(PORT);

            hostInfo = new HostInfo
            {
                ServiceMode = ServiceMode.Server,
                DeviceName = deviceInfo.DeviceName,
                OS = deviceInfo.OS,
                Address = NetworkUtil.GetPrimaryIPv4Address(),
                MultiCastAddress = server.MulticastIPAddress.ToString()
            };

            broadcasting = new ServerBroadcastingController(PORT - 1, hostInfo);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            server.Start();
            broadcasting.ReceiveBroadcast();

            stopButton.IsEnabled = true;
            mainWindow.IsPaneVisible = startButton.IsEnabled = false;
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            server.Stop();
            broadcasting.StopReceivingBroadcast();

            stopButton.IsEnabled = false;
            mainWindow.IsPaneVisible = startButton.IsEnabled = true;
        }
    }
}
