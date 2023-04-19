using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using RemoteAudio.Core.Networking;
using RemoteAudio.Windows.Pages;
using System;
using System.Linq;

namespace RemoteAudio.Windows
{
    public sealed partial class MainWindow : Window
    {
        public bool IsPaneVisible
        {
            get => navigationView.IsPaneVisible;
            set => navigationView.IsPaneVisible = value;
        }

        public MainWindow()
        {
            InitializeComponent();

            Title = "Remote Audio";
            SystemBackdrop = new MicaBackdrop();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
        }

        private void navigationView_Loaded(object sender, RoutedEventArgs e)
        {
            navigationView.SelectedItem = navigationView.MenuItems.FirstOrDefault();
            navigate("Server", null);
        }

        private void navigate(string tag, NavigationTransitionInfo transitionInfo)
        {
            Type page = Type.GetType($"RemoteAudio.Windows.Pages.{tag}Page");

            if (page == null)
                return;

            contentFrame.Navigate(page, null, transitionInfo);
        }

        private void contentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is ServerPage)
            {
                App.DisposeUdpClients();
                App.InitializeUdpClient(ServiceMode.Server);
            }
            else if (e.Content is ClientPage)
            {
                App.DisposeUdpClients();
                App.InitializeUdpClient(ServiceMode.Client, h => (e.Content as ClientPage).ListView.Items.Add(h));
            }
        }

        private void navigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs e)
        {
            navigate(e.SelectedItemContainer.Tag?.ToString(), e.RecommendedNavigationTransitionInfo);
        }
    }
}