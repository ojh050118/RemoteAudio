using Microsoft.UI.Xaml;
using RemoteAudio.Core.Audio.Windows;
using RemoteAudio.Core.Networking;
using RemoteAudio.Core.Networking.Server;
using RemoteAudio.Server.Windows.Helper;

namespace RemoteAudio.Server.Windows
{
    public partial class App : Application
    {
        public static HostInfo HostInfo { get; set; }
        public static UdpAudioServer Server { get; set; }
        public static AudioListener Listener { get; set; }
        public static RemoteAudioBroadcastingController Broadcasting { get; set; }

        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            mainWindow = new MainWindow();
            WindowHelper.TrackWindow(mainWindow);
            mainWindow.Activate();
        }

        private Window mainWindow;
    }
}