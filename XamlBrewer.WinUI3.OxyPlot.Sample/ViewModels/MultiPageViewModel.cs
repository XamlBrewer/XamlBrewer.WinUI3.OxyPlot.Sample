using CommunityToolkit.Mvvm.ComponentModel;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamlBrewer.WinUI3.OxyPlot.Sample.ViewModels
{
    internal class MultiPageViewModel : ObservableObject
    {
        public PlotModel BarSeriesModel
        {
            get
            {
                var model = new PlotModel(); // { Title = "Cake Type Popularity" };

                //generate a random percentage distribution between the 5
                //cake-types (see axis below)
                var rand = new Random();
                double[] cakePopularity = new double[5];
                for (int i = 0; i < 5; ++i)
                {
                    cakePopularity[i] = rand.NextDouble();
                }
                var sum = cakePopularity.Sum();

                var barSeries = new BarSeries
                {
                    ItemsSource = new List<BarItem>(new[]
                        {
                            new BarItem{ Value = (cakePopularity[0] / sum * 100) },
                            new BarItem{ Value = (cakePopularity[1] / sum * 100) },
                            new BarItem{ Value = (cakePopularity[2] / sum * 100) },
                            new BarItem{ Value = (cakePopularity[3] / sum * 100) },
                            new BarItem{ Value = (cakePopularity[4] / sum * 100) }
                        }),
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

                return model;
            }
        }

        public PlotModel ContourSeriesModel
        {
            get {
                var model = new PlotModel(); // { Title = "ContourSeries" };

                double x0 = -3.1;
                double x1 = 3.1;
                double y0 = -3;
                double y1 = 3;

                //generate values
                Func<double, double, double> peaks = (x, y) => 3 * (1 - x) * (1 - x) * Math.Exp(-(x * x) - (y + 1) * (y + 1)) - 10 * (x / 5 - x * x * x - y * y * y * y * y) * Math.Exp(-x * x - y * y) - 1.0 / 3 * Math.Exp(-(x + 1) * (x + 1) - y * y);
                var xx = ArrayBuilder.CreateVector(x0, x1, 100);
                var yy = ArrayBuilder.CreateVector(y0, y1, 100);
                var peaksData = ArrayBuilder.Evaluate(peaks, xx, yy);

                var cs = new ContourSeries
                {
                    Color = OxyColors.Black,
                    LabelBackground = OxyColors.White,
                    ColumnCoordinates = yy,
                    RowCoordinates = xx,
                    Data = peaksData
                };
                model.Series.Add(cs);

                return model;
            }
        }

        public PlotModel BitmapHeatMapSeriesModel
        {
            get
            {
                var model = new PlotModel(); // { Title = "Heatmap" };

                // Color axis (the X and Y axes are generated automatically)
                model.Axes.Add(new LinearColorAxis
                {
                    Palette = OxyPalettes.Rainbow(100)
                });

                // generate 1d normal distribution
                var singleData = new double[100];
                for (int x = 0; x < 100; ++x)
                {
                    singleData[x] = Math.Exp((-1.0 / 2.0) * Math.Pow(((double)x - 50.0) / 20.0, 2.0));
                }

                // generate 2d normal distribution
                var data = new double[100, 100];
                for (int x = 0; x < 100; ++x)
                {
                    for (int y = 0; y < 100; ++y)
                    {
                        data[y, x] = singleData[x] * singleData[(y + 30) % 100] * 100;
                    }
                }

                var heatMapSeries = new HeatMapSeries
                {
                    X0 = 0,
                    X1 = 99,
                    Y0 = 0,
                    Y1 = 99,
                    Interpolate = true,
                    RenderMethod = HeatMapRenderMethod.Bitmap,
                    Data = data
                };

                model.Series.Add(heatMapSeries);
                return model;
            }
        }

        public PlotModel RectangularHeatMapSeriesModel
        {
            get
            {
                var model = new PlotModel(); // { Title = "Hmmmm ... cookies" };

                // Weekday axis (horizontal)
                model.Axes.Add(new CategoryAxis
                {
                    Position = AxisPosition.Bottom,

                    // Key used for specifying this axis in the HeatMapSeries
                    Key = "WeekdayAxis",

                    // Array of Categories (see above), mapped to one of the coordinates of the 2D-data array
                    ItemsSource = new[]
                    {
                        "Monday",
                        "Tuesday",
                        "Wednesday",
                        "Thursday",
                        "Friday",
                        "Saturday",
                        "Sunday"
                    }
                });

                // Cake type axis (vertical)
                model.Axes.Add(new CategoryAxis
                {
                    Position = AxisPosition.Left,
                    Key = "CakeAxis",
                    ItemsSource = new[]
                    {
                        "Apple cake",
                        "Baumkuchen",
                        "Bundt cake",
                        "Chocolate cake",
                        "Carrot cake"
                    }
                });

                // Color axis
                model.Axes.Add(new LinearColorAxis
                {
                    Palette = OxyPalettes.Hot(200)
                });

                var rand = new Random();
                var data = new double[7, 5];
                for (int x = 0; x < 5; ++x)
                {
                    for (int y = 0; y < 7; ++y)
                    {
                        data[y, x] = rand.Next(0, 200) * (0.13 * (y + 1));
                    }
                }

                var heatMapSeries = new HeatMapSeries
                {
                    X0 = 0,
                    X1 = 6,
                    Y0 = 0,
                    Y1 = 4,
                    XAxisKey = "WeekdayAxis",
                    YAxisKey = "CakeAxis",
                    RenderMethod = HeatMapRenderMethod.Rectangles,
                    LabelFontSize = 0.2, // neccessary to display the label
                    Data = data
                };

                model.Series.Add(heatMapSeries);

                return model;
            }
        }

        public PlotModel FunctionSeriesModel
        {
            get
            {
                var model = new PlotModel(); // { Title = "Nana nana nana nana nana nana nana nana" };

                Func<double, double> batFn1 = (x) => 2 * Math.Sqrt(-Math.Abs(Math.Abs(x) - 1) * Math.Abs(3 - Math.Abs(x)) / ((Math.Abs(x) - 1) * (3 - Math.Abs(x)))) * (1 + Math.Abs(Math.Abs(x) - 3) / (Math.Abs(x) - 3)) * Math.Sqrt(1 - Math.Pow((x / 7), 2)) + (5 + 0.97 * (Math.Abs(x - 0.5) + Math.Abs(x + 0.5)) - 3 * (Math.Abs(x - 0.75) + Math.Abs(x + 0.75))) * (1 + Math.Abs(1 - Math.Abs(x)) / (1 - Math.Abs(x)));
                Func<double, double> batFn2 = (x) => -3 * Math.Sqrt(1 - Math.Pow((x / 7), 2)) * Math.Sqrt(Math.Abs(Math.Abs(x) - 4) / (Math.Abs(x) - 4));
                Func<double, double> batFn3 = (x) => Math.Abs(x / 2) - 0.0913722 * (Math.Pow(x, 2)) - 3 + Math.Sqrt(1 - Math.Pow((Math.Abs(Math.Abs(x) - 2) - 1), 2));
                Func<double, double> batFn4 = (x) => (2.71052 + (1.5 - .5 * Math.Abs(x)) - 1.35526 * Math.Sqrt(4 - Math.Pow((Math.Abs(x) - 1), 2))) * Math.Sqrt(Math.Abs(Math.Abs(x) - 1) / (Math.Abs(x) - 1)) + 0.9;

                model.Series.Add(new FunctionSeries(batFn1, -8, 8, 0.0001));
                model.Series.Add(new FunctionSeries(batFn2, -8, 8, 0.0001));
                model.Series.Add(new FunctionSeries(batFn3, -8, 8, 0.0001));
                model.Series.Add(new FunctionSeries(batFn4, -8, 8, 0.0001));

                model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
                model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1 });

                return model;
            }
        }

        public PlotModel PieSeriesModel
        {
            get {
                var model = new PlotModel(); // { Title = "Pie Sample1" };

                dynamic seriesP1 = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };

                seriesP1.Slices.Add(new PieSlice("Africa", 1030) { IsExploded = false, Fill = OxyColors.PaleVioletRed });
                seriesP1.Slices.Add(new PieSlice("Americas", 929) { IsExploded = true });
                seriesP1.Slices.Add(new PieSlice("Asia", 4157) { IsExploded = true });
                seriesP1.Slices.Add(new PieSlice("Europe", 739) { IsExploded = true });
                seriesP1.Slices.Add(new PieSlice("Oceania", 35) { IsExploded = true });

                model.Series.Add(seriesP1);

                return model;
            }
        }

        public PlotModel StemSeriesModel
        {
            get {
                var model = new PlotModel(); // { Title = "Trigonometric functions" };

                var start = -Math.PI;
                var end = Math.PI;
                var step = 0.1;
                int steps = (int)((Math.Abs(start) + Math.Abs(end)) / step);

                //generate points for functions
                var sinData = new DataPoint[steps];
                for (int i = 0; i < steps; ++i)
                {
                    var x = (start + step * i);
                    sinData[i] = new DataPoint(x, Math.Sin(x));
                }

                //sin(x)
                var sinStemSeries = new StemSeries
                {
                    MarkerStroke = OxyColors.Green,
                    MarkerType = MarkerType.Circle
                };
                sinStemSeries.Points.AddRange(sinData);

                model.Series.Add(sinStemSeries);

                return model;
            }
        }
    }
}
