using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XamlBrewer.WinUI3.OxyPlot.Sample.Views
{
    public sealed partial class ThemingPage : Page
    {
        private PlotModel helloWorldmodel;
        private PlotModel boxPlotModel;

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

        public PlotModel HelloWorldModel => helloWorldmodel;

        public PlotModel BoxPlotModel => boxPlotModel;

        private void InitializePlotModel()
        {
            helloWorldmodel = new PlotModel
            {
                Title = "Hello WinUI 3",
                PlotAreaBorderColor = OxyColors.Transparent,
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

            foreach (var axis in helloWorldmodel.Axes)
            {
                axis.AxislineStyle = LineStyle.Solid;
            }

            const int boxes = 10;

            boxPlotModel = new PlotModel(); // (string.Format("BoxPlot (n={0})", boxes)) { LegendPlacement = LegendPlacement.Outside };

            var s1 = new BoxPlotSeries
            {
                Title = "BoxPlotSeries",
                BoxWidth = 0.3
            };

            var random = new Random();
            for (var i = 0; i < boxes; i++)
            {
                double x = i;
                var points = 5 + random.Next(15);
                var values = new List<double>();
                for (var j = 0; j < points; j++)
                {
                    values.Add(random.Next(0, 20));
                }

                values.Sort();
                var median = GetMedian(values);
                int r = values.Count % 2;
                double firstQuartil = GetMedian(values.Take((values.Count + r) / 2));
                double thirdQuartil = GetMedian(values.Skip((values.Count - r) / 2));

                var iqr = thirdQuartil - firstQuartil;
                var step = iqr * 1.5;
                var upperWhisker = thirdQuartil + step;
                upperWhisker = values.Where(v => v <= upperWhisker).Max();
                var lowerWhisker = firstQuartil - step;
                lowerWhisker = values.Where(v => v >= lowerWhisker).Min();

                var outliers = values.Where(v => v > upperWhisker || v < lowerWhisker).ToList();

                s1.Items.Add(new BoxPlotItem(x, lowerWhisker, firstQuartil, median, thirdQuartil, upperWhisker)
                {
                    Outliers = outliers
                });
            }

            boxPlotModel.Series.Add(s1);
            boxPlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            boxPlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0.1, MaximumPadding = 0.1 });

            foreach (var axis in boxPlotModel.Axes)
            {
                axis.AxislineStyle = LineStyle.Solid;
            }

        }

        private static double GetMedian(IEnumerable<double> values)
        {
            var sortedInterval = new List<double>(values);
            sortedInterval.Sort();
            var count = sortedInterval.Count;
            if (count % 2 == 1)
            {
                return sortedInterval[(count - 1) / 2];
            }

            return 0.5 * sortedInterval[count / 2] + 0.5 * sortedInterval[(count / 2) - 1];
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
                helloWorldmodel.TextColor = OxyColors.DimGray;
                // helloWorldmodel.PlotAreaBorderColor = OxyColors.DimGray;
                foreach (var axis in helloWorldmodel.Axes)
                {
                    axis.TicklineColor = OxyColors.DimGray;
                    axis.AxislineColor = OxyColors.DimGray;
                }
                // model.DefaultColors = OxyPalettes.Cool(model.DefaultColors.Count).Colors;

                boxPlotModel.TextColor = OxyColors.DimGray;
                boxPlotModel.PlotAreaBorderColor = OxyColors.Transparent;
                foreach (var axis in boxPlotModel.Axes)
                {
                    axis.TicklineColor = OxyColors.DimGray;
                    axis.AxislineColor = OxyColors.DimGray;
                    //axis.color
                }
                var series = boxPlotModel.Series[0] as BoxPlotSeries;
                series.Stroke = OxyColors.LightSlateGray;
                series.Fill = OxyColors.LightSteelBlue;
            }
            else
            {
                helloWorldmodel.TextColor = OxyColors.Beige;
                // helloWorldmodel.PlotAreaBorderColor = OxyColors.Beige;
                foreach (var axis in helloWorldmodel.Axes)
                {
                    axis.TicklineColor = OxyColors.Beige;
                    axis.AxislineColor = OxyColors.Beige;
                }
                // model.DefaultColors = OxyPalettes.Hot(model.DefaultColors.Count).Colors;

                boxPlotModel.TextColor = OxyColors.Beige;
                boxPlotModel.PlotAreaBorderColor = OxyColors.Transparent;
                foreach (var axis in boxPlotModel.Axes)
                {
                    axis.TicklineColor = OxyColors.Beige;
                    axis.AxislineColor = OxyColors.Beige;
                }
                var series = boxPlotModel.Series[0] as BoxPlotSeries;
                series.Fill = OxyColors.LightSlateGray;
                series.Stroke = OxyColors.LightSteelBlue;
            }

            helloWorldmodel.InvalidatePlot(false);
            boxPlotModel.InvalidatePlot(true);
        }
    }
}
