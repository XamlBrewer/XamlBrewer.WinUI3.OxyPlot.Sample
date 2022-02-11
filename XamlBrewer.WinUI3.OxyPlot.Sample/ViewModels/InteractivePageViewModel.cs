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
        private int value = 0;
        private PlotModel model = new PlotModel();

        public InteractivePageViewModel()
        {
            var barSeries = new BarSeries
            {
                ItemsSource = new List<BarItem>(),
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0:.00}%"
            };

            model.Series.Add(barSeries);

            model.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                Key = "CakeAxis",
                ItemsSource = new[]
                    {
                        "Apple cake",
                        "Baumkuchen",
                        "Bundt Cake",
                        "Chocolate cake",
                        "Carrot cake"
                    }
            });
        }

        public int Value
        {
            get => value;
            set
            {
                SetProperty(ref value, value);
                updateSeries();
            }
        }

        public PlotModel Model => model;

        private void updateSeries()
        {
            //generate a random percentage distribution between the 5
            //cake-types (see axis below)
            var rand = new Random();
            double[] cakePopularity = new double[5];
            for (int i = 0; i < 5; ++i)
            {
                cakePopularity[i] = rand.NextDouble();
            }

            var sum = cakePopularity.Sum();

            (model.Series.First() as BarSeries).ItemsSource = new List<BarItem>(new[]
                {
                    new BarItem{ Value = (cakePopularity[0] / sum * 100) },
                    new BarItem{ Value = (cakePopularity[1] / sum * 100) },
                    new BarItem{ Value = (cakePopularity[2] / sum * 100) },
                    new BarItem{ Value = (cakePopularity[3] / sum * 100) },
                    new BarItem{ Value = (cakePopularity[4] / sum * 100) }
                });

            model.InvalidatePlot(true);
        }
    }
}
