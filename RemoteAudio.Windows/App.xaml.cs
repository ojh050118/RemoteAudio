using Microsoft.UI.Xaml;
using RemoteAudio.Core.Audio.Windows;
using RemoteAudio.Core.Networking;
using RemoteAudio.Core.Networking.Server;
using RemoteAudio.Core.Platform;
using RemoteAudio.Core.Utils;
using RemoteAudio.Windows.Helper;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RemoteAudio.Windows
{
    public partial class App : Application
    {
        public const int PORT = 6974;

        public static HostInfo HostInfo { get; set; }
        public static UdpAudioServer Server { get; set; }
        public static AudioListener Listener { get; set; }
        public static RemoteAudioBroadcastingController Broadcasting { get; set; }
        public static IDeviceInfo DeviceInfo;

        private static Action<HostInfo> dataReceived;

        public App()
        {
            InitializeComponent();
            
            DeviceInfo = PlatformUtil.GetDeviceInfo();
            HostInfo = HostInfo.GetHostInfo(ServiceMode.Server);
            Broadcasting = new RemoteAudioBroadcastingController(PORT - 1, HostInfo, ServiceMode.Client);
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
        }

        public static async Task InitializeUdpClient(ServiceMode serviceMode, Action<HostInfo> dataReceived = null)
        {
            HostInfo.ServiceMode = serviceMode;
            Broadcasting.DataReceived -= App.dataReceived;
            Broadcasting.DataReceived += App.dataReceived = dataReceived;

            switch (serviceMode)
            {
                case ServiceMode.Server:
                    await Task.CompletedTask;
                    Server = new UdpAudioServer(PORT, IPAddress.Parse(HostInfo.MultiCastAddress));

                    Broadcasting.TargetServiceMode = ServiceMode.Client;
                    Broadcasting.DataReceived += serverDataReceived;

                    break;

                case ServiceMode.Client:
                    await Task.CompletedTask;
                    Broadcasting.TargetServiceMode = ServiceMode.Server;
                    Broadcasting.DataReceived -= serverDataReceived;

                    break;
            }
        }

        private static void serverDataReceived(HostInfo info)
        {
            Broadcasting.Broadcast();
        }

        private Window mainWindow;
    }
}