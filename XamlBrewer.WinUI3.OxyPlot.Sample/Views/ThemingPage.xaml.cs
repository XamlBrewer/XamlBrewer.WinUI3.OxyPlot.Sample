﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using XamlBrewer.WinUI3.Services.Theming;

namespace XamlBrewer.WinUI3.OxyPlot.Sample.Views
{
    public sealed partial class ThemingPage : Page
    {
        private PlotModel helloWorldmodel;
        private PlotModel boxPlotModel;
        private PlotModel pieChartModel;

        public ThemingPage()
        {
            InitializeComponent();
            InitializePlotModel();
            InitializePieChartModel();

            Loaded += ThemingPage_Loaded;
        }

        private void ThemingPage_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyTheme(ActualTheme);

            foreach (var slice in (pieChartModel.Series.First() as PieSeries).Slices)
            {
                var color = slice.ActualFillColor;
                slice.Fill = OxyColor.FromArgb(90, color.R, color.G, color.B);
            }
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

        public PlotModel HelloWorldModel => helloWorldmodel;

        public PlotModel BoxPlotModel => boxPlotModel;

        public PlotModel PieChartModel => pieChartModel;

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

            boxPlotModel.PlotAreaBorderColor = OxyColors.Transparent;

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

        private void InitializePieChartModel()
        {
            pieChartModel = new PlotModel(); // { Title = "Pie Sample1" };

            pieChartModel.PlotAreaBorderColor = OxyColors.Transparent;

            var seriesP1 = new PieSeries { InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };

            seriesP1.Slices.Add(new PieSlice("Africa", 1030) { IsExploded = false });
            seriesP1.Slices.Add(new PieSlice("Americas", 929) { IsExploded = true });
            seriesP1.Slices.Add(new PieSlice("Asia", 4157) { IsExploded = true });
            seriesP1.Slices.Add(new PieSlice("Europe", 739) { IsExploded = true });
            seriesP1.Slices.Add(new PieSlice("Oceania", 35) { IsExploded = true });

            pieChartModel.Series.Add(seriesP1);
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

        private void Page_ActualThemeChanged(FrameworkElement sender, object args)
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
            // ...

            // Beware: Do not use OxyColors.Black and OxyColors.White.
            // Their cached brushes are reversed, based on the Theme. Confusing!
            var foreground = theme == ElementTheme.Light ? OxyColor.FromRgb(32, 32, 32) : OxyColors.WhiteSmoke;

            helloWorldmodel.TextColor = foreground;
            foreach (var axis in helloWorldmodel.Axes)
            {
                axis.TicklineColor = foreground;
                axis.AxislineColor = foreground;
            }

            // There's room for an extension method ...
            boxPlotModel.ApplyTheme(theme);


            // Specific changes.
            var series = boxPlotModel.Series[0] as BoxPlotSeries;
            if (theme == ElementTheme.Light)
            {
                series.Fill = OxyColors.LightSteelBlue;
                series.Stroke = OxyColors.LightSlateGray;
            }
            else
            {
                series.Fill = OxyColors.LightSlateGray;
                series.Stroke = OxyColors.LightSteelBlue;
            }

            pieChartModel.ApplyTheme(theme);

            helloWorldmodel.InvalidatePlot(false);
            boxPlotModel.InvalidatePlot(false);
        }
    }
}
