using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using System.Runtime.InteropServices;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RemoteAudio.Server.Windows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private WindowsSystemDispatcherQueueHelper queueHelper;
        private SystemBackdropConfiguration systemBackdropConfiguration;
        private DesktopAcrylicController desktopAcrylicController;
        private MicaController micaController;
        public MainWindow()
        {
            this.InitializeComponent();
            trySetAcrylicBackdrop();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
        }

        private bool trySetAcrylicBackdrop()
        {
            if (DesktopAcrylicController.IsSupported())
            {
                queueHelper = new WindowsSystemDispatcherQueueHelper();
                queueHelper.EnsureWindowsSystemDispatcherQueueController();

                systemBackdropConfiguration = new SystemBackdropConfiguration();
                Activated += Window_Activated;
                Closed += Window_Closed;
                ((FrameworkElement)Content).ActualThemeChanged += Window_ThemeChanged;

                // Initial configuration state.
                systemBackdropConfiguration.IsInputActive = true;
                SetConfigurationSourceTheme();

                micaController = new MicaController();

                desktopAcrylicController = new DesktopAcrylicController();

                // Enable the system backdrop.
                // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
                desktopAcrylicController.AddSystemBackdropTarget(this.As<ICompositionSupportsSystemBackdrop>());
                desktopAcrylicController.SetSystemBackdropConfiguration(systemBackdropConfiguration);
                return true; // succeeded
            }

            return false; // Acrylic is not supported on this system
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            systemBackdropConfiguration.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            // Make sure any Mica/Acrylic controller is disposed so it doesn't try to
            // use this closed window.
            if (micaController != null)
            {
                micaController.Dispose();
                micaController = null;
            }
            Activated -= Window_Activated;
            systemBackdropConfiguration = null;
        }


        private void Window_ThemeChanged(FrameworkElement sender, object args)
        {
            if (systemBackdropConfiguration != null)
            {
                SetConfigurationSourceTheme();
            }
        }

        private void SetConfigurationSourceTheme()
        {
            switch (((FrameworkElement)Content).ActualTheme)
            {
                case ElementTheme.Dark: systemBackdropConfiguration.Theme = SystemBackdropTheme.Dark; break;
                case ElementTheme.Light: systemBackdropConfiguration.Theme = SystemBackdropTheme.Light; break;
                case ElementTheme.Default: systemBackdropConfiguration.Theme = SystemBackdropTheme.Default; break;
            }
        }
    }

    public class WindowsSystemDispatcherQueueHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        struct DispatcherQueueOptions
        {
            internal int dwSize;
            internal int threadType;
            internal int apartmentType;
        }

        [DllImport("CoreMessaging.dll")]
        private static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object dispatcherQueueController);

        object m_dispatcherQueueController = null;
        public void EnsureWindowsSystemDispatcherQueueController()
        {
            /*if (Windows.System.DispatcherQueue.GetForCurrentThread() != null)
            {
                // one already exists, so we'll just use it.
                return;
            }*/

            if (m_dispatcherQueueController == null)
            {
                DispatcherQueueOptions options;
                options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
                options.threadType = 2;    // DQTYPE_THREAD_CURRENT
                options.apartmentType = 2; // DQTAT_COM_STA

                CreateDispatcherQueueController(options, ref m_dispatcherQueueController);
            }
        }
    }
}
