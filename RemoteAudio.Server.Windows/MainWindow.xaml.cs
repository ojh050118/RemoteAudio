using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;

namespace RemoteAudio.Server.Windows
{
    public sealed partial class MainWindow : Window
    {
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
        }

        private void navigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs e)
        {
            navigate(e.InvokedItemContainer.Tag?.ToString(), e.RecommendedNavigationTransitionInfo);
        }

        private void navigate(string tag, NavigationTransitionInfo transitionInfo)
        {
            Type page = Type.GetType($"RemoteAudio.Server.Windows.Pages.{tag}Page");

            if (page == null)
                return;

            contentFrame.Navigate(page, null, transitionInfo);
        }

        private void contentFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}