using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RemoteAudio.Windows.Helper;

namespace RemoteAudio.Windows.Pages
{
    public sealed partial class DebugPage : Page
    {
        public DebugPage()
        {
            InitializeComponent();
            titleBarSwitch.Loaded += (_, __) =>
            {
                var window = WindowHelper.GetWindowForElement(this);
                titleBarSwitch.IsOn = window.ExtendsContentIntoTitleBar;
            };
        }

        private void titleBarSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var window = WindowHelper.GetWindowForElement(this) as MainWindow;

            if (titleBarSwitch.IsOn)
            {
                window.ExtendsContentIntoTitleBar = true;
                window.SetTitleBar(window.AppTitleBar);
            }
            else
            {
                window.ExtendsContentIntoTitleBar = false;
                window.SetTitleBar(null);
            }
        }
    }
}
