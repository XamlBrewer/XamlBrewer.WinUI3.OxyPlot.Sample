using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using XamlBrewer.WinUI3.Services.Theming;

namespace XamlBrewer.WinUI3.OxyPlot.Sample.Views
{
    public sealed partial class InteractivePage : Page
    {
        public InteractivePage()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyTheme(ActualTheme);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ActualThemeChanged += Page_ActualThemeChanged;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ActualThemeChanged -= Page_ActualThemeChanged;
            base.OnNavigatedFrom(e);
        }

        private void Page_ActualThemeChanged(FrameworkElement sender, object args)
        {
            ApplyTheme(sender.ActualTheme);
        }

        private void ApplyTheme(ElementTheme theme)
        {
            ViewModel.Model.ApplyTheme(theme);
        }
    }
}
