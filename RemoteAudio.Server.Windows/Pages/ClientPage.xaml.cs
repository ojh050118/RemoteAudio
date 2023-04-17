using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RemoteAudio.Core.Audio.Windows;
using RemoteAudio.Core.Networking;
using RemoteAudio.Core.Platform;
using RemoteAudio.Core.Utils;
using RemoteAudio.Server.Windows.Helper;
using System.Linq;
using System.Net;

namespace RemoteAudio.Server.Windows.Pages
{
    public sealed partial class ClientPage : Page
    {
        public const int PORT = 6974;

        private IDeviceInfo deviceInfo;
        private MainWindow mainWindow;

        private HostInfo selected;

        public ClientPage()
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

            App.HostInfo = HostInfo.GetHostInfo(ServiceMode.Client);
            App.Broadcasting = new RemoteAudioBroadcastingController(PORT - 1, App.HostInfo, ServiceMode.Server);

            App.Broadcasting.DataReceived += h =>
            {
                listView.Items.Add(h);
            };
            listView.Items.Add(App.HostInfo);
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            App.Listener = new AudioListener(new IPEndPoint(IPAddress.Parse(selected.MultiCastAddress), PORT));
            App.Listener.Start();

            disconnectButton.IsEnabled = true;
            mainWindow.IsPaneVisible = connectButton.IsEnabled = false;
        }

        private void disconnectButton_Click(object sender, RoutedEventArgs e)
        {
            App.Listener?.Dispose();

            disconnectButton.IsEnabled = false;
            mainWindow.IsPaneVisible = connectButton.IsEnabled = true;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = e.AddedItems.FirstOrDefault() as HostInfo;

            if (selected != null && !disconnectButton.IsEnabled)
                connectButton.IsEnabled = true;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            App.Broadcasting.Broadcast();
            App.Broadcasting.ReceiveBroadcast();
        }
    }
}
