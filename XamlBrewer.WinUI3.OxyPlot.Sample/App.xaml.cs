﻿using Microsoft.UI.Xaml;
using XamlBrewer.WinUI3.Services;

namespace XamlBrewer.WinUI3.OxyPlot.Sample
{
    public partial class App : Application
    {
        private Shell shell;

        public App()
        {
            InitializeComponent();
        }

        public INavigation Navigation => shell;

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            shell = new Shell();
            shell.Activate();
        }
    }
}
