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

namespace XamlBrewer.WinUI3.OxyPlot.Sample
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Window
    {
        public Shell()
        {
            Title = "XAML Brewer WinUI 3 OxyPlot Sample";

            InitializeComponent();

            (Application.Current as App).EnsureSettings();
            ApplyTheme();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var settings = (Application.Current as App).Settings;
            settings.IsLightTheme = !settings.IsLightTheme;
            (Application.Current as App).SaveSettings();
            Root.ActualThemeChanged += Root_ActualThemeChanged;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var settings = (Application.Current as App).Settings;
            Root.RequestedTheme = settings.IsLightTheme ? ElementTheme.Light : ElementTheme.Dark;
        }
        private void Root_ActualThemeChanged(FrameworkElement sender, object args)
        {
            // Theme change refinements (e.g. content dialogs and title bar).
        }
    }
}
