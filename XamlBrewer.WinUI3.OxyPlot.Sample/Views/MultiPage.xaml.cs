using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using XamlBrewer.WinUI3.Services.Theming;

namespace XamlBrewer.WinUI3.OxyPlot.Sample.Views
{
    public sealed partial class MultiPage : Page
    {
        public MultiPage()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyTheme(/* (Content as FrameworkElement).*/ ActualTheme);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            /* (Content as FrameworkElement).*/ ActualThemeChanged += Page_ActualThemeChanged;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            /* (Content as FrameworkElement).*/ ActualThemeChanged -= Page_ActualThemeChanged;
            base.OnNavigatedFrom(e);
        }

        private void Page_ActualThemeChanged(FrameworkElement sender, object args)
        {
            ApplyTheme(sender.ActualTheme);
        }

        private void ApplyTheme(ElementTheme theme)
        {
            ViewModel.AreaSeriesModel.ApplyTheme(theme);
            ViewModel.BarSeriesModel.ApplyTheme(theme);
            ViewModel.BitmapHeatMapSeriesModel.ApplyTheme(theme);
            ViewModel.ContourSeriesModel.ApplyTheme(theme);
            ViewModel.FunctionSeriesModel.ApplyTheme(theme);
            ViewModel.LineAndAreaSeriesModel.ApplyTheme(theme);
            ViewModel.PieSeriesModel.ApplyTheme(theme);
            ViewModel.RectangularHeatMapSeriesModel.ApplyTheme(theme);
            ViewModel.StemSeriesModel.ApplyTheme(theme);
        }
    }
}
