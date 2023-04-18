using Microsoft.UI.Xaml;
using RemoteAudio.Core.Audio.Windows;
using RemoteAudio.Core.Networking;
using RemoteAudio.Core.Networking.Server;
using RemoteAudio.Server.Windows.Helper;
using System;
using System.Net;

namespace RemoteAudio.Server.Windows
{
    public partial class App : Application
    {
        public const int PORT = 6974;

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

        public static void DisposeUdpClients()
        {
            Server?.Dispose();
            Listener?.Dispose();
            Broadcasting?.Dispose();
        }

        public static void InitializeUdpClient(ServiceMode serviceMode, Action<HostInfo> dataReceived = null)
        {
            switch (serviceMode)
            {
                case ServiceMode.Server:
                    HostInfo = HostInfo.GetHostInfo(ServiceMode.Server);
                    Server = new UdpAudioServer(PORT, IPAddress.Parse(HostInfo.MultiCastAddress));
                    Broadcasting = new ServerBroadcastingController(PORT - 1, HostInfo);
                    Broadcasting.DataReceived += dataReceived;

                    break;

                case ServiceMode.Client:
                    HostInfo = HostInfo.GetHostInfo(ServiceMode.Client);
                    Broadcasting = new RemoteAudioBroadcastingController(PORT - 1, HostInfo, ServiceMode.Server);
                    Broadcasting.DataReceived += dataReceived;

                    break;
            }
        }

        private Window mainWindow;
    }
}