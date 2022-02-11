using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace XamlBrewer.WinUI3.OxyPlot.Sample.Views
{
    public sealed partial class ThemingPage : Page
    {
        private PlotModel model;

        public ThemingPage()
        {
            InitializeComponent();
            InitializePlotModel();

            Loaded += ThemingPage_Loaded;
        }

        private void ThemingPage_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyTheme((Content as FrameworkElement).ActualTheme);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            (Content as FrameworkElement).ActualThemeChanged += ThemingPage_ActualThemeChanged;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            (Content as FrameworkElement).ActualThemeChanged -= ThemingPage_ActualThemeChanged;
            base.OnNavigatedFrom(e);
        }

        public PlotModel Model => model;

        private void InitializePlotModel()
        {
            model = new PlotModel
            {
                Title = "Hello WinUI 3",
                Axes =
            {
                new LinearAxis { Position = AxisPosition.Bottom },
                new LinearAxis { Position = AxisPosition.Left },
            },
                Series =
            {
                new LineSeries
                {
                    Title = "LineSeries",
                    MarkerType = MarkerType.Circle,
                    Points =
                    {
                        new DataPoint(0, 0),
                        new DataPoint(10, 18),
                        new DataPoint(20, 12),
                        new DataPoint(30, 8),
                        new DataPoint(40, 15),
                    }
                }
            }
            };
        }

        private void ThemingPage_ActualThemeChanged(FrameworkElement sender, object args)
        {
            ApplyTheme(sender.ActualTheme);
        }

        private void ApplyTheme(ElementTheme theme)
        {
            // model.DefaultColors
            // model.SelectionColor
            // model.SubtitleColor
            // model.TextColor
            // model.TitleColor
            // model.PlotAreaBorderColor
            // model.PlotAreaBackground
            // model.Background
            // model.Axes[0].AxislineColor
            // model.Axes[0].ExtraGridlineColor
            // model.Axes[0].MajorGridlineColor
            // model.Axes[0].MinorGridlineColor
            // model.Axes[0].MinorTicklineColor
            // model.Axes[0].TicklineColor
            // model.Axes[0].TextColor
            // model.Axes[0].TitleColor
            // model.Legends[0].TextColor
            // model.Legends[0].LegendTextColor
            // model.Legends[0].LegendTitleColor
            // model.Legends[0].LegendBackground

            if (theme == ElementTheme.Light)
            {
                model.TextColor = OxyColors.DimGray;
                model.PlotAreaBorderColor = OxyColors.DimGray;
                // model.DefaultColors = OxyPalettes.Cool(model.DefaultColors.Count).Colors;
            }
            else
            {
                model.TextColor = OxyColors.Beige;
                model.PlotAreaBorderColor = OxyColors.Beige;
                // model.DefaultColors = OxyPalettes.Hot(model.DefaultColors.Count).Colors;
            }

            model.InvalidatePlot(false);
        }
    }
}
