using Microsoft.UI.Xaml;
using RemoteAudio.Server.Windows.Helper;

namespace RemoteAudio.Server.Windows
{
    public partial class App : Application
    {
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