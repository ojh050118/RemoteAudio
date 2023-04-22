using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using RemoteAudio.Core.Audio.Windows;
using RemoteAudio.Core.Networking;
using RemoteAudio.Windows.Helper;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RemoteAudio.Windows.Pages
{
    public sealed partial class ClientPage : Page
    {
        private MainWindow mainWindow;

        private HostInfo selected;

        public ClientPage()
        {
            InitializeComponent();

            Loaded += (_, __) =>
            {
                mainWindow = WindowHelper.GetWindowForElement(this) as MainWindow;
            };

            ListView.Items.Add(App.HostInfo);
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            App.Listener = new AudioListener(new IPEndPoint(IPAddress.Parse(selected.MultiCastAddress), App.PORT));
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await Task.Run(() => App.InitializeUdpClient(ServiceMode.Client, h => ListView.Items.Add(h)));

            serviceModeText.Text = App.HostInfo.ServiceMode.ToString();
            addressText.Text = App.HostInfo.Address;
            multicastAddressText.Text = App.HostInfo.MultiCastAddress;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            App.DisposeUdpClients();
        }
    }
}
