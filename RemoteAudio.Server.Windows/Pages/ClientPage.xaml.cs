using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using RemoteAudio.Core.Audio.Windows;
using RemoteAudio.Core.Networking;
using RemoteAudio.Core.Networking.Server;
using RemoteAudio.Core.Platform;
using RemoteAudio.Core.Utils;
using RemoteAudio.Server.Windows.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Xamarin.Essentials;

namespace RemoteAudio.Server.Windows.Pages
{
    public sealed partial class ClientPage : Page
    {
        public const int PORT = 6974;

        private IDeviceInfo deviceInfo;
        private MainWindow mainWindow;

        public ClientPage()
        {
            InitializeComponent();

            deviceInfo = PlatformUtil.GetDeviceInfo();
            Loaded += (_, __) =>
            {
                mainWindow = WindowHelper.GetWindowForElement(this) as MainWindow;
            };

            listView.Loaded += (_, __) => listView.Items.Add(App.HostInfo);

            App.Server?.Dispose();
            App.Listener?.Dispose();
            App.Broadcasting?.Dispose();

            App.HostInfo = HostInfo.GetHostInfo(ServiceMode.Client);
            App.Broadcasting = new RemoteAudioBroadcastingController(PORT - 1, App.HostInfo, ServiceMode.Server);

            App.Broadcasting.DataReceived += h =>
            {
                listView.Items.Add(h);
            };
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            disconnectButton.IsEnabled = true;
            mainWindow.IsPaneVisible = connectButton.IsEnabled = false;
        }

        private void disconnectButton_Click(object sender, RoutedEventArgs e)
        {
            disconnectButton.IsEnabled = false;
            mainWindow.IsPaneVisible = connectButton.IsEnabled = true;
        }
    }
}
