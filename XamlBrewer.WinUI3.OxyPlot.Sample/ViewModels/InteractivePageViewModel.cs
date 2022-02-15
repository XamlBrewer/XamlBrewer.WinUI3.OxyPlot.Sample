using CommunityToolkit.Mvvm.ComponentModel;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XamlBrewer.WinUI3.OxyPlot.Sample.ViewModels
{
    internal partial class InteractivePageViewModel : ObservableObject
    {
        private double variance = 0;
        private PlotModel model = new PlotModel();

        public InteractivePageViewModel()
        {
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = -0.05,
                Maximum = .95,
                MajorStep = 0.2,
                MinorStep = 0.05,
                TickStyle = TickStyle.Inside
            });
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = -5,
                Maximum = 5,
                MajorStep = 1,
                MinorStep = 0.25,
                TickStyle = TickStyle.Inside
            });

            model.PlotAreaBorderColor = OxyColors.Transparent;

            updateSeries();
        }

        public double Variance
        {
            get => variance;
            set
            {
                SetProperty(ref variance, value);
                updateSeries();
            }
        }

        public PlotModel Model => model;

        private void updateSeries()
        {
            model.Series.Clear();
            model.Series.Add(CreateNormalDistributionSeries(-5, 5, Variance, 0.2));
            model.Series.Add(CreateNormalDistributionSeries(-5, 5, -5, 5 + Variance));
            model.Series.Add(CreateNormalDistributionSeries(-5, 5, 5, 5 - Variance));

            model.InvalidatePlot(true);
        }

        private static DataPointSeries CreateNormalDistributionSeries(double x0, double x1, double mean, double variance, int n = 100)
        {
            var ls = new StemSeries
            {
                // MarkerStroke = OxyColors.Green,
                MarkerType = MarkerType.Circle,
                Title = string.Format("μ={0}, σ²={1}", mean, variance)
            };

            for (int i = 0; i < n; i++)
            {
                double x = x0 + ((x1 - x0) * i / (n - 1));
                double f = 1.0 / Math.Sqrt(2 * Math.PI * variance) * Math.Exp(-(x - mean) * (x - mean) / 2 / variance);
                ls.Points.Add(new DataPoint(x, f));
            }

            return ls;
        }
    }
}
