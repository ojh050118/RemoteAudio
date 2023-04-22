using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using RemoteAudio.Core.Networking;
using System.Threading.Tasks;

namespace RemoteAudio.Windows.Pages
{
    public sealed partial class ServerPage : Page
    {
        public ServerPage()
        {
            InitializeComponent();

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
            startButton.IsEnabled = descriptionBox.IsEnabled = false;
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            App.Server.Stop();
            App.Broadcasting.StopReceivingBroadcast();

            stopButton.IsEnabled = false;
            startButton.IsEnabled = descriptionBox.IsEnabled = true;
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

            stopButton_Click(null, null);
            App.DisposeUdpClients();
        }
    }
}
