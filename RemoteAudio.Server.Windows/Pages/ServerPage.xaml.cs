using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RemoteAudio.Core.Networking;
using RemoteAudio.Core.Platform;
using RemoteAudio.Core.Utils;
using RemoteAudio.Core.Networking.Server;
using RemoteAudio.Server.Windows.Helper;
using System.Net;

namespace RemoteAudio.Server.Windows.Pages
{
    public sealed partial class ServerPage : Page
    {
        public const int PORT = 6974;

        private IDeviceInfo deviceInfo;
        private MainWindow mainWindow;

        public ServerPage()
        {
            InitializeComponent();

            deviceInfo = PlatformUtil.GetDeviceInfo();
            Loaded += (_, __) =>
            {
                mainWindow = WindowHelper.GetWindowForElement(this) as MainWindow;
            };

            App.Server?.Dispose();
            App.Listener?.Dispose();
            App.Broadcasting?.Dispose();

            App.HostInfo = HostInfo.GetHostInfo(ServiceMode.Server);
            App.Server = new UdpAudioServer(PORT, IPAddress.Parse(App.HostInfo.MultiCastAddress));
            App.Broadcasting = new ServerBroadcastingController(PORT - 1, App.HostInfo);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            App.Server.Start();
            App.Broadcasting.ReceiveBroadcast();

            stopButton.IsEnabled = true;
            mainWindow.IsPaneVisible = startButton.IsEnabled = false;
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            App.Server.Stop();
            App.Broadcasting.StopReceivingBroadcast();

            stopButton.IsEnabled = false;
            mainWindow.IsPaneVisible = startButton.IsEnabled = true;
        }
    }
}