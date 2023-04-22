using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using RemoteAudio.Core.Networking;
using RemoteAudio.Windows.Helper;
using System.Threading.Tasks;

namespace RemoteAudio.Windows.Pages
{
    public sealed partial class ServerPage : Page
    {
        private MainWindow mainWindow;

        public ServerPage()
        {
            InitializeComponent();

            Loaded += (_, __) =>
            {
                mainWindow = WindowHelper.GetWindowForElement(this) as MainWindow;
            };
            descriptionBox.TextChanged += (s, e) =>
            {
                App.HostInfo.Description = descriptionText.Text = descriptionBox.Text;
            };
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            App.Server.Start();
            App.Broadcasting.ReceiveBroadcast();

            stopButton.IsEnabled = true;
            mainWindow.IsPaneVisible = startButton.IsEnabled = descriptionBox.IsEnabled = false;
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            App.Server.Stop();
            App.Broadcasting.StopReceivingBroadcast();

            stopButton.IsEnabled = false;
            mainWindow.IsPaneVisible = startButton.IsEnabled = descriptionBox.IsEnabled = true;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await Task.Run(() => App.InitializeUdpClient(ServiceMode.Server));

            serviceModeText.Text = App.HostInfo.ServiceMode.ToString();
            descriptionText.Text = App.HostInfo.Description;
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
