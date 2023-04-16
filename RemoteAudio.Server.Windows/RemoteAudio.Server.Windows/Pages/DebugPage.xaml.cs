// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using RemoteAudio.Server.Windows.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RemoteAudio.Server.Windows.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
