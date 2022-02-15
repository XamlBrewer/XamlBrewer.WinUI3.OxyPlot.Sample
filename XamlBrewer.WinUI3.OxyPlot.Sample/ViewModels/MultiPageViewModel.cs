using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using Windows.UI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XamlBrewer.WinUI3.OxyPlot.Sample.ViewModels
{
    internal class MultiPageViewModel : ObservableObject
    {
        private PlotModel areaSeriesModel;
        private PlotModel barSeriesModel;
        private PlotModel bitmapHeatMapSeriesModel;
        private PlotModel contourSeriesModel;
        private PlotModel functionSeriesModel;
        private PlotModel lineAndAreaSeriesModel;
        private PlotModel lineSeriesModel;
        private PlotModel multiValueAxesBarSeriesModel;
        private PlotModel pieSeriesModel;
        private PlotModel rectangularHeatMapSeriesModel;
        private PlotModel stemSeriesModel;
        private PlotModel twoColorAreaSeriesModel;

        public MultiPageViewModel()
        {
            InitializeAreaSeriesModel();
            InitializeBarSeriesModel();
            InitializeBitmapHeatMapSeriesModel();
            InitializeContourSeriesModel();
            InitializeFunctionSeriesModel();
            InitializeLineAndAreaSeriesModel();
            InitializeLineSeriesModel();
            InitializeMultiValueAxesBarSeriesModel();
            InitializePieSeriesModel();
            InitializeRectangularHeatmapSeriesModel();
            InitializeStemSeriesModel();
            InitializeTwoColorAreaSeriesModel();
        }

        public PlotModel AreaSeriesModel => areaSeriesModel;

        public PlotModel BarSeriesModel => barSeriesModel;

        public PlotModel BitmapHeatMapSeriesModel => bitmapHeatMapSeriesModel;

        public PlotModel ContourSeriesModel => contourSeriesModel;

        public PlotModel FunctionSeriesModel => functionSeriesModel;
        
        public PlotModel LineAndAreaSeriesModel => lineAndAreaSeriesModel; 
        
        public PlotModel LineSeriesModel => lineSeriesModel;
        
        public PlotModel MultiValueAxesBarSeriesModel => multiValueAxesBarSeriesModel;

        public PlotModel PieSeriesModel => pieSeriesModel;

        public PlotModel RectangularHeatMapSeriesModel => rectangularHeatMapSeriesModel;

        public PlotModel StemSeriesModel => stemSeriesModel;

        public PlotModel TwoColorAreaSeriesModel => twoColorAreaSeriesModel;


        private static DataPointSeries CreateNormalDistributionSeries(double x0, double x1, double mean, double variance, int n = 1001)
        {
            var ls = new LineSeries
            {
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

        private void InitializeAreaSeriesModel()
        {
            areaSeriesModel = new PlotModel(); // { Title = "AreaSeries with different stroke colors" };
            areaSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;
            var areaSeries1 = new AreaSeries();
            areaSeries1.Points.Add(new DataPoint(0, 50));
            areaSeries1.Points.Add(new DataPoint(10, 40));
            areaSeries1.Points.Add(new DataPoint(20, 60));
            areaSeries1.Points2.Add(new DataPoint(0, 60));
            areaSeries1.Points2.Add(new DataPoint(5, 80));
            areaSeries1.Points2.Add(new DataPoint(20, 70));
            areaSeries1.Color = OxyColors.Red;
            areaSeries1.Color2 = OxyColors.Blue;
            areaSeriesModel.Series.Add(areaSeries1);
        }

        private void InitializeBarSeriesModel()
        {
            barSeriesModel = new PlotModel(); // { Title = "Cake Type Popularity" };

            barSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;

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
                LabelFormatString = "{0:.00}%",
                TextColor = OxyColors.WhiteSmoke
            };
            barSeriesModel.Series.Add(barSeries);

            barSeriesModel.Axes.Add(new CategoryAxis
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

        private void InitializeBitmapHeatMapSeriesModel()
        {
            bitmapHeatMapSeriesModel = new PlotModel(); // { Title = "Heatmap" };

            bitmapHeatMapSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;

            // Color axis (the X and Y axes are generated automatically)
            bitmapHeatMapSeriesModel.Axes.Add(new LinearColorAxis
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

            bitmapHeatMapSeriesModel.Series.Add(heatMapSeries);
        }

        private void InitializeContourSeriesModel()
        {
            contourSeriesModel = new PlotModel(); // { Title = "ContourSeries" };

            contourSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;

            double x0 = -3.1;
            double x1 = 3.1;
            double y0 = -3;
            double y1 = 3;

            //generate values
            Func<double, double, double> peaks = (x, y) => 3 * (1 - x) * (1 - x) * Math.Exp(-(x * x) - (y + 1) * (y + 1)) - 10 * (x / 5 - x * x * x - y * y * y * y * y) * Math.Exp(-x * x - y * y) - 1.0 / 3 * Math.Exp(-(x + 1) * (x + 1) - y * y);
            var xx = ArrayBuilder.CreateVector(x0, x1, 100);
            var yy = ArrayBuilder.CreateVector(y0, y1, 100);
            var peaksData = ArrayBuilder.Evaluate(peaks, xx, yy);

            var accentColor = (Color)Application.Current.Resources["SystemAccentColor"];

            var cs = new ContourSeries
            {
                //Color = OxyColors.Black,
                Color = OxyColor.FromArgb(accentColor.A, accentColor.R, accentColor.G, accentColor.B),
                LabelBackground = OxyColors.Transparent,
                ColumnCoordinates = yy,
                RowCoordinates = xx,
                Data = peaksData
            };
            contourSeriesModel.Series.Add(cs);
        }

        private void InitializeFunctionSeriesModel()
        {
            functionSeriesModel = new PlotModel(); // { Title = "Nana nana nana nana nana nana nana nana" };

            functionSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;

            Func<double, double> batFn1 = (x) => 2 * Math.Sqrt(-Math.Abs(Math.Abs(x) - 1) * Math.Abs(3 - Math.Abs(x)) / ((Math.Abs(x) - 1) * (3 - Math.Abs(x)))) * (1 + Math.Abs(Math.Abs(x) - 3) / (Math.Abs(x) - 3)) * Math.Sqrt(1 - Math.Pow((x / 7), 2)) + (5 + 0.97 * (Math.Abs(x - 0.5) + Math.Abs(x + 0.5)) - 3 * (Math.Abs(x - 0.75) + Math.Abs(x + 0.75))) * (1 + Math.Abs(1 - Math.Abs(x)) / (1 - Math.Abs(x)));
            Func<double, double> batFn2 = (x) => -3 * Math.Sqrt(1 - Math.Pow((x / 7), 2)) * Math.Sqrt(Math.Abs(Math.Abs(x) - 4) / (Math.Abs(x) - 4));
            Func<double, double> batFn3 = (x) => Math.Abs(x / 2) - 0.0913722 * (Math.Pow(x, 2)) - 3 + Math.Sqrt(1 - Math.Pow((Math.Abs(Math.Abs(x) - 2) - 1), 2));
            Func<double, double> batFn4 = (x) => (2.71052 + (1.5 - .5 * Math.Abs(x)) - 1.35526 * Math.Sqrt(4 - Math.Pow((Math.Abs(x) - 1), 2))) * Math.Sqrt(Math.Abs(Math.Abs(x) - 1) / (Math.Abs(x) - 1)) + 0.9;

            functionSeriesModel.Series.Add(new FunctionSeries(batFn1, -8, 8, 0.0001));
            functionSeriesModel.Series.Add(new FunctionSeries(batFn2, -8, 8, 0.0001));
            functionSeriesModel.Series.Add(new FunctionSeries(batFn3, -8, 8, 0.0001));
            functionSeriesModel.Series.Add(new FunctionSeries(batFn4, -8, 8, 0.0001));

            functionSeriesModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
            functionSeriesModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1 });
        }

        private void InitializeLineAndAreaSeriesModel()
        {
            lineAndAreaSeriesModel = new PlotModel(); // { Title = "LineSeries and AreaSeries" };
            lineAndAreaSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;
            var linearAxis1 = new LinearAxis { Position = AxisPosition.Bottom };
            lineAndAreaSeriesModel.Axes.Add(linearAxis1);
            var linearAxis2 = new LinearAxis();
            lineAndAreaSeriesModel.Axes.Add(linearAxis2);
            var areaSeries1 = new AreaSeries
            {
                Fill = OxyColors.LightBlue,
                DataFieldX2 = "Time",
                DataFieldY2 = "Minimum",
                Color = OxyColors.Red,
                StrokeThickness = 0,
                MarkerFill = OxyColors.Transparent,
                DataFieldX = "Time",
                DataFieldY = "Maximum"
            };
            areaSeries1.Points2.Add(new DataPoint(0, -5.04135905692417));
            areaSeries1.Points2.Add(new DataPoint(2.5, -4.91731850813018));
            areaSeries1.Points2.Add(new DataPoint(5, -4.45266314658926));
            areaSeries1.Points2.Add(new DataPoint(7.5, -3.87303874542613));
            areaSeries1.Points2.Add(new DataPoint(10, -3.00101110255393));
            areaSeries1.Points2.Add(new DataPoint(12.5, -2.17980725503518));
            areaSeries1.Points2.Add(new DataPoint(15, -1.67332229254456));
            areaSeries1.Points2.Add(new DataPoint(17.5, -1.10537158549082));
            areaSeries1.Points2.Add(new DataPoint(20, -0.6145459544447));
            areaSeries1.Points2.Add(new DataPoint(22.5, 0.120028106039404));
            areaSeries1.Points2.Add(new DataPoint(25, 1.06357270435597));
            areaSeries1.Points2.Add(new DataPoint(27.5, 1.87301405606466));
            areaSeries1.Points2.Add(new DataPoint(30, 2.57569854952195));
            areaSeries1.Points2.Add(new DataPoint(32.5, 3.59165537664278));
            areaSeries1.Points2.Add(new DataPoint(35, 4.87991958133872));
            areaSeries1.Points2.Add(new DataPoint(37.5, 6.36214537958714));
            areaSeries1.Points2.Add(new DataPoint(40, 7.62564585126268));
            areaSeries1.Points2.Add(new DataPoint(42.5, 8.69606320261772));
            areaSeries1.Points2.Add(new DataPoint(45, 10.0118704438265));
            areaSeries1.Points2.Add(new DataPoint(47.5, 11.0434480519236));
            areaSeries1.Points2.Add(new DataPoint(50, 11.9794171576758));
            areaSeries1.Points2.Add(new DataPoint(52.5, 12.9591851832621));
            areaSeries1.Points2.Add(new DataPoint(55, 14.172107889304));
            areaSeries1.Points2.Add(new DataPoint(57.5, 15.5520057698488));
            areaSeries1.Points2.Add(new DataPoint(60, 17.2274942386092));
            areaSeries1.Points2.Add(new DataPoint(62.5, 18.6983982186757));
            areaSeries1.Points2.Add(new DataPoint(65, 20.4560332001448));
            areaSeries1.Points2.Add(new DataPoint(67.5, 22.4867327382261));
            areaSeries1.Points2.Add(new DataPoint(70, 24.5319674302041));
            areaSeries1.Points2.Add(new DataPoint(72.5, 26.600547815813));
            areaSeries1.Points2.Add(new DataPoint(75, 28.5210891459701));
            areaSeries1.Points2.Add(new DataPoint(77.5, 30.6793080755413));
            areaSeries1.Points2.Add(new DataPoint(80, 33.0546651200646));
            areaSeries1.Points2.Add(new DataPoint(82.5, 35.3256065179713));
            areaSeries1.Points2.Add(new DataPoint(85, 37.6336074839968));
            areaSeries1.Points2.Add(new DataPoint(87.5, 40.2012266359763));
            areaSeries1.Points2.Add(new DataPoint(90, 42.8923555399256));
            areaSeries1.Points2.Add(new DataPoint(92.5, 45.8665211907432));
            areaSeries1.Points2.Add(new DataPoint(95, 48.8200195945427));
            areaSeries1.Points2.Add(new DataPoint(97.5, 51.8304284402311));
            areaSeries1.Points2.Add(new DataPoint(100, 54.6969868542147));
            areaSeries1.Points2.Add(new DataPoint(102.5, 57.7047292990632));
            areaSeries1.Points2.Add(new DataPoint(105, 60.4216644602929));
            areaSeries1.Points2.Add(new DataPoint(107.5, 62.926258762519));
            areaSeries1.Points2.Add(new DataPoint(110, 65.1829734629407));
            areaSeries1.Points2.Add(new DataPoint(112.5, 67.2365592083133));
            areaSeries1.Points2.Add(new DataPoint(115, 69.5713628691022));
            areaSeries1.Points2.Add(new DataPoint(117.5, 71.7267046705944));
            areaSeries1.Points2.Add(new DataPoint(120, 73.633463102781));
            areaSeries1.Points2.Add(new DataPoint(122.5, 75.4660150158061));
            areaSeries1.Points2.Add(new DataPoint(125, 77.5669292504745));
            areaSeries1.Points2.Add(new DataPoint(127.5, 79.564218544664));
            areaSeries1.Points2.Add(new DataPoint(130, 81.8631309028078));
            areaSeries1.Points2.Add(new DataPoint(132.5, 83.9698189969034));
            areaSeries1.Points2.Add(new DataPoint(135, 86.3847886532009));
            areaSeries1.Points2.Add(new DataPoint(137.5, 88.5559348267764));
            areaSeries1.Points2.Add(new DataPoint(140, 91.0455050418365));
            areaSeries1.Points2.Add(new DataPoint(142.5, 93.6964157585504));
            areaSeries1.Points2.Add(new DataPoint(145, 96.284336864941));
            areaSeries1.Points2.Add(new DataPoint(147.5, 98.7508602689723));
            areaSeries1.Points2.Add(new DataPoint(150, 100.904510594255));
            areaSeries1.Points2.Add(new DataPoint(152.5, 103.266136681506));
            areaSeries1.Points2.Add(new DataPoint(155, 105.780951269521));
            areaSeries1.Points2.Add(new DataPoint(157.5, 108.032859257065));
            areaSeries1.Points2.Add(new DataPoint(160, 110.035478448093));
            areaSeries1.Points2.Add(new DataPoint(162.5, 112.10655731615));
            areaSeries1.Points2.Add(new DataPoint(165, 114.37480786097));
            areaSeries1.Points2.Add(new DataPoint(167.5, 116.403992550869));
            areaSeries1.Points2.Add(new DataPoint(170, 118.61663988727));
            areaSeries1.Points2.Add(new DataPoint(172.5, 120.538730287384));
            areaSeries1.Points2.Add(new DataPoint(175, 122.515721057177));
            areaSeries1.Points2.Add(new DataPoint(177.5, 124.474386629124));
            areaSeries1.Points2.Add(new DataPoint(180, 126.448283293214));
            areaSeries1.Points2.Add(new DataPoint(182.5, 128.373811322299));
            areaSeries1.Points2.Add(new DataPoint(185, 130.33627914667));
            areaSeries1.Points2.Add(new DataPoint(187.5, 132.487933658477));
            areaSeries1.Points2.Add(new DataPoint(190, 134.716989778456));
            areaSeries1.Points2.Add(new DataPoint(192.5, 136.817287595392));
            areaSeries1.Points2.Add(new DataPoint(195, 139.216488664698));
            areaSeries1.Points2.Add(new DataPoint(197.5, 141.50803227574));
            areaSeries1.Points2.Add(new DataPoint(200, 143.539586683614));
            areaSeries1.Points2.Add(new DataPoint(202.5, 145.535911545221));
            areaSeries1.Points2.Add(new DataPoint(205, 147.516964978686));
            areaSeries1.Points2.Add(new DataPoint(207.5, 149.592416731684));
            areaSeries1.Points2.Add(new DataPoint(210, 151.600983566512));
            areaSeries1.Points2.Add(new DataPoint(212.5, 153.498210993362));
            areaSeries1.Points2.Add(new DataPoint(215, 155.512606828247));
            areaSeries1.Points2.Add(new DataPoint(217.5, 157.426564302774));
            areaSeries1.Points2.Add(new DataPoint(220, 159.364474964172));
            areaSeries1.Points2.Add(new DataPoint(222.5, 161.152806492128));
            areaSeries1.Points2.Add(new DataPoint(225, 162.679069434562));
            areaSeries1.Points2.Add(new DataPoint(227.5, 163.893622036741));
            areaSeries1.Points2.Add(new DataPoint(230, 165.475827621238));
            areaSeries1.Points2.Add(new DataPoint(232.5, 167.303960444734));
            areaSeries1.Points2.Add(new DataPoint(235, 169.259393394952));
            areaSeries1.Points2.Add(new DataPoint(237.5, 171.265193646758));
            areaSeries1.Points2.Add(new DataPoint(240, 173.074304345192));
            areaSeries1.Points2.Add(new DataPoint(242.5, 174.975492766814));
            areaSeries1.Points2.Add(new DataPoint(245, 176.684088218484));
            areaSeries1.Points2.Add(new DataPoint(247.5, 178.406887247603));
            areaSeries1.Points.Add(new DataPoint(0, 5.0184649433561));
            areaSeries1.Points.Add(new DataPoint(2.5, 5.27685959268215));
            areaSeries1.Points.Add(new DataPoint(5, 5.81437064628786));
            areaSeries1.Points.Add(new DataPoint(7.5, 6.51022475040994));
            areaSeries1.Points.Add(new DataPoint(10, 7.49921246878766));
            areaSeries1.Points.Add(new DataPoint(12.5, 8.41941631823751));
            areaSeries1.Points.Add(new DataPoint(15, 9.09826907222079));
            areaSeries1.Points.Add(new DataPoint(17.5, 9.89500750098145));
            areaSeries1.Points.Add(new DataPoint(20, 10.6633345249404));
            areaSeries1.Points.Add(new DataPoint(22.5, 11.6249613445368));
            areaSeries1.Points.Add(new DataPoint(25, 12.8816391467497));
            areaSeries1.Points.Add(new DataPoint(27.5, 13.9665185705603));
            areaSeries1.Points.Add(new DataPoint(30, 14.8501816818724));
            areaSeries1.Points.Add(new DataPoint(32.5, 16.0683128022441));
            areaSeries1.Points.Add(new DataPoint(35, 17.5378799723172));
            areaSeries1.Points.Add(new DataPoint(37.5, 19.1262752954039));
            areaSeries1.Points.Add(new DataPoint(40, 20.4103953650735));
            areaSeries1.Points.Add(new DataPoint(42.5, 21.5430627723891));
            areaSeries1.Points.Add(new DataPoint(45, 22.9105459463366));
            areaSeries1.Points.Add(new DataPoint(47.5, 23.9802361888719));
            areaSeries1.Points.Add(new DataPoint(50, 24.8659461235003));
            areaSeries1.Points.Add(new DataPoint(52.5, 25.7303194442439));
            areaSeries1.Points.Add(new DataPoint(55, 26.7688545912359));
            areaSeries1.Points.Add(new DataPoint(57.5, 28.0545112571933));
            areaSeries1.Points.Add(new DataPoint(60, 29.7036634266394));
            areaSeries1.Points.Add(new DataPoint(62.5, 31.2273634344467));
            areaSeries1.Points.Add(new DataPoint(65, 33.1038196356519));
            areaSeries1.Points.Add(new DataPoint(67.5, 35.2639893610328));
            areaSeries1.Points.Add(new DataPoint(70, 37.434293559489));
            areaSeries1.Points.Add(new DataPoint(72.5, 39.7109359368267));
            areaSeries1.Points.Add(new DataPoint(75, 41.7573881676222));
            areaSeries1.Points.Add(new DataPoint(77.5, 44.0460374479862));
            areaSeries1.Points.Add(new DataPoint(80, 46.5098714746581));
            areaSeries1.Points.Add(new DataPoint(82.5, 48.7754012129155));
            areaSeries1.Points.Add(new DataPoint(85, 51.1619816926597));
            areaSeries1.Points.Add(new DataPoint(87.5, 53.9036778414639));
            areaSeries1.Points.Add(new DataPoint(90, 56.7448825012636));
            areaSeries1.Points.Add(new DataPoint(92.5, 59.9294987878434));
            areaSeries1.Points.Add(new DataPoint(95, 63.0148831289797));
            areaSeries1.Points.Add(new DataPoint(97.5, 66.0721745989622));
            areaSeries1.Points.Add(new DataPoint(100, 68.8980036274521));
            areaSeries1.Points.Add(new DataPoint(102.5, 71.7719322611447));
            areaSeries1.Points.Add(new DataPoint(105, 74.4206055336728));
            areaSeries1.Points.Add(new DataPoint(107.5, 76.816198386632));
            areaSeries1.Points.Add(new DataPoint(110, 79.0040432726983));
            areaSeries1.Points.Add(new DataPoint(112.5, 80.9617606926066));
            areaSeries1.Points.Add(new DataPoint(115, 83.1345574620341));
            areaSeries1.Points.Add(new DataPoint(117.5, 85.0701022046479));
            areaSeries1.Points.Add(new DataPoint(120, 86.8557530286516));
            areaSeries1.Points.Add(new DataPoint(122.5, 88.5673387745243));
            areaSeries1.Points.Add(new DataPoint(125, 90.6003321543338));
            areaSeries1.Points.Add(new DataPoint(127.5, 92.439864576254));
            areaSeries1.Points.Add(new DataPoint(130, 94.5383744861178));
            areaSeries1.Points.Add(new DataPoint(132.5, 96.4600166864507));
            areaSeries1.Points.Add(new DataPoint(135, 98.6091052949006));
            areaSeries1.Points.Add(new DataPoint(137.5, 100.496459351478));
            areaSeries1.Points.Add(new DataPoint(140, 102.705767030085));
            areaSeries1.Points.Add(new DataPoint(142.5, 105.009994476992));
            areaSeries1.Points.Add(new DataPoint(145, 107.31287026052));
            areaSeries1.Points.Add(new DataPoint(147.5, 109.584842542272));
            areaSeries1.Points.Add(new DataPoint(150, 111.641435600837));
            areaSeries1.Points.Add(new DataPoint(152.5, 113.988459973544));
            areaSeries1.Points.Add(new DataPoint(155, 116.50349048027));
            areaSeries1.Points.Add(new DataPoint(157.5, 118.753612704274));
            areaSeries1.Points.Add(new DataPoint(160, 120.801728924085));
            areaSeries1.Points.Add(new DataPoint(162.5, 122.902486914165));
            areaSeries1.Points.Add(new DataPoint(165, 125.104391935796));
            areaSeries1.Points.Add(new DataPoint(167.5, 127.06056966547));
            areaSeries1.Points.Add(new DataPoint(170, 129.217086578495));
            areaSeries1.Points.Add(new DataPoint(172.5, 131.151968896274));
            areaSeries1.Points.Add(new DataPoint(175, 133.159906275133));
            areaSeries1.Points.Add(new DataPoint(177.5, 135.065263957561));
            areaSeries1.Points.Add(new DataPoint(180, 137.041870026822));
            areaSeries1.Points.Add(new DataPoint(182.5, 138.937477489811));
            areaSeries1.Points.Add(new DataPoint(185, 140.776914926282));
            areaSeries1.Points.Add(new DataPoint(187.5, 142.786975776398));
            areaSeries1.Points.Add(new DataPoint(190, 144.862762377347));
            areaSeries1.Points.Add(new DataPoint(192.5, 146.89654967049));
            areaSeries1.Points.Add(new DataPoint(195, 149.204343821204));
            areaSeries1.Points.Add(new DataPoint(197.5, 151.369748673527));
            areaSeries1.Points.Add(new DataPoint(200, 153.324438580137));
            areaSeries1.Points.Add(new DataPoint(202.5, 155.173148715344));
            areaSeries1.Points.Add(new DataPoint(205, 157.0501827528));
            areaSeries1.Points.Add(new DataPoint(207.5, 159.109122278359));
            areaSeries1.Points.Add(new DataPoint(210, 161.044446932778));
            areaSeries1.Points.Add(new DataPoint(212.5, 162.942364031841));
            areaSeries1.Points.Add(new DataPoint(215, 164.966769883021));
            areaSeries1.Points.Add(new DataPoint(217.5, 166.89711806788));
            areaSeries1.Points.Add(new DataPoint(220, 168.906874949069));
            areaSeries1.Points.Add(new DataPoint(222.5, 170.85692034995));
            areaSeries1.Points.Add(new DataPoint(225, 172.602125010408));
            areaSeries1.Points.Add(new DataPoint(227.5, 173.964258466598));
            areaSeries1.Points.Add(new DataPoint(230, 175.629908385654));
            areaSeries1.Points.Add(new DataPoint(232.5, 177.495778359378));
            areaSeries1.Points.Add(new DataPoint(235, 179.432933300749));
            areaSeries1.Points.Add(new DataPoint(237.5, 181.400180771342));
            areaSeries1.Points.Add(new DataPoint(240, 183.232300309899));
            areaSeries1.Points.Add(new DataPoint(242.5, 185.225502661441));
            areaSeries1.Points.Add(new DataPoint(245, 186.979590140413));
            areaSeries1.Points.Add(new DataPoint(247.5, 188.816640077725));
            areaSeries1.Title = "Maximum/Minimum";
            lineAndAreaSeriesModel.Series.Add(areaSeries1);

            var lineSeries1 = new LineSeries
            {
                Color = OxyColors.Blue,
                MarkerFill = OxyColors.Transparent,
                DataFieldX = "Time",
                DataFieldY = "Value"
            };
            lineSeries1.Points.Add(new DataPoint(0, -0.011447056784037));
            lineSeries1.Points.Add(new DataPoint(2.5, 0.179770542275985));
            lineSeries1.Points.Add(new DataPoint(5, 0.6808537498493));
            lineSeries1.Points.Add(new DataPoint(7.5, 1.31859300249191));
            lineSeries1.Points.Add(new DataPoint(10, 2.24910068311687));
            lineSeries1.Points.Add(new DataPoint(12.5, 3.11980453160117));
            lineSeries1.Points.Add(new DataPoint(15, 3.71247338983811));
            lineSeries1.Points.Add(new DataPoint(17.5, 4.39481795774531));
            lineSeries1.Points.Add(new DataPoint(20, 5.02439428524784));
            lineSeries1.Points.Add(new DataPoint(22.5, 5.87249472528812));
            lineSeries1.Points.Add(new DataPoint(25, 6.97260592555283));
            lineSeries1.Points.Add(new DataPoint(27.5, 7.91976631331247));
            lineSeries1.Points.Add(new DataPoint(30, 8.71294011569719));
            lineSeries1.Points.Add(new DataPoint(32.5, 9.82998408944345));
            lineSeries1.Points.Add(new DataPoint(35, 11.208899776828));
            lineSeries1.Points.Add(new DataPoint(37.5, 12.7442103374955));
            lineSeries1.Points.Add(new DataPoint(40, 14.0180206081681));
            lineSeries1.Points.Add(new DataPoint(42.5, 15.1195629875034));
            lineSeries1.Points.Add(new DataPoint(45, 16.4612081950815));
            lineSeries1.Points.Add(new DataPoint(47.5, 17.5118421203978));
            lineSeries1.Points.Add(new DataPoint(50, 18.4226816405881));
            lineSeries1.Points.Add(new DataPoint(52.5, 19.344752313753));
            lineSeries1.Points.Add(new DataPoint(55, 20.47048124027));
            lineSeries1.Points.Add(new DataPoint(57.5, 21.8032585135211));
            lineSeries1.Points.Add(new DataPoint(60, 23.4655788326243));
            lineSeries1.Points.Add(new DataPoint(62.5, 24.9628808265612));
            lineSeries1.Points.Add(new DataPoint(65, 26.7799264178984));
            lineSeries1.Points.Add(new DataPoint(67.5, 28.8753610496295));
            lineSeries1.Points.Add(new DataPoint(70, 30.9831304948466));
            lineSeries1.Points.Add(new DataPoint(72.5, 33.1557418763199));
            lineSeries1.Points.Add(new DataPoint(75, 35.1392386567962));
            lineSeries1.Points.Add(new DataPoint(77.5, 37.3626727617638));
            lineSeries1.Points.Add(new DataPoint(80, 39.7822682973613));
            lineSeries1.Points.Add(new DataPoint(82.5, 42.0505038654434));
            lineSeries1.Points.Add(new DataPoint(85, 44.3977945883283));
            lineSeries1.Points.Add(new DataPoint(87.5, 47.0524522387201));
            lineSeries1.Points.Add(new DataPoint(90, 49.8186190205946));
            lineSeries1.Points.Add(new DataPoint(92.5, 52.8980099892933));
            lineSeries1.Points.Add(new DataPoint(95, 55.9174513617612));
            lineSeries1.Points.Add(new DataPoint(97.5, 58.9513015195966));
            lineSeries1.Points.Add(new DataPoint(100, 61.7974952408334));
            lineSeries1.Points.Add(new DataPoint(102.5, 64.738330780104));
            lineSeries1.Points.Add(new DataPoint(105, 67.4211349969828));
            lineSeries1.Points.Add(new DataPoint(107.5, 69.8712285745755));
            lineSeries1.Points.Add(new DataPoint(110, 72.0935083678195));
            lineSeries1.Points.Add(new DataPoint(112.5, 74.0991599504599));
            lineSeries1.Points.Add(new DataPoint(115, 76.3529601655682));
            lineSeries1.Points.Add(new DataPoint(117.5, 78.3984034376212));
            lineSeries1.Points.Add(new DataPoint(120, 80.2446080657163));
            lineSeries1.Points.Add(new DataPoint(122.5, 82.0166768951652));
            lineSeries1.Points.Add(new DataPoint(125, 84.0836307024042));
            lineSeries1.Points.Add(new DataPoint(127.5, 86.002041560459));
            lineSeries1.Points.Add(new DataPoint(130, 88.2007526944628));
            lineSeries1.Points.Add(new DataPoint(132.5, 90.2149178416771));
            lineSeries1.Points.Add(new DataPoint(135, 92.4969469740507));
            lineSeries1.Points.Add(new DataPoint(137.5, 94.5261970891274));
            lineSeries1.Points.Add(new DataPoint(140, 96.875636035961));
            lineSeries1.Points.Add(new DataPoint(142.5, 99.3532051177711));
            lineSeries1.Points.Add(new DataPoint(145, 101.798603562731));
            lineSeries1.Points.Add(new DataPoint(147.5, 104.167851405622));
            lineSeries1.Points.Add(new DataPoint(150, 106.272973097546));
            lineSeries1.Points.Add(new DataPoint(152.5, 108.627298327525));
            lineSeries1.Points.Add(new DataPoint(155, 111.142220874895));
            lineSeries1.Points.Add(new DataPoint(157.5, 113.39323598067));
            lineSeries1.Points.Add(new DataPoint(160, 115.418603686089));
            lineSeries1.Points.Add(new DataPoint(162.5, 117.504522115157));
            lineSeries1.Points.Add(new DataPoint(165, 119.739599898383));
            lineSeries1.Points.Add(new DataPoint(167.5, 121.732281108169));
            lineSeries1.Points.Add(new DataPoint(170, 123.916863232882));
            lineSeries1.Points.Add(new DataPoint(172.5, 125.845349591829));
            lineSeries1.Points.Add(new DataPoint(175, 127.837813666155));
            lineSeries1.Points.Add(new DataPoint(177.5, 129.769825293343));
            lineSeries1.Points.Add(new DataPoint(180, 131.745076660018));
            lineSeries1.Points.Add(new DataPoint(182.5, 133.655644406055));
            lineSeries1.Points.Add(new DataPoint(185, 135.556597036476));
            lineSeries1.Points.Add(new DataPoint(187.5, 137.637454717438));
            lineSeries1.Points.Add(new DataPoint(190, 139.789876077902));
            lineSeries1.Points.Add(new DataPoint(192.5, 141.856918632941));
            lineSeries1.Points.Add(new DataPoint(195, 144.210416242951));
            lineSeries1.Points.Add(new DataPoint(197.5, 146.438890474634));
            lineSeries1.Points.Add(new DataPoint(200, 148.432012631876));
            lineSeries1.Points.Add(new DataPoint(202.5, 150.354530130282));
            lineSeries1.Points.Add(new DataPoint(205, 152.283573865743));
            lineSeries1.Points.Add(new DataPoint(207.5, 154.350769505022));
            lineSeries1.Points.Add(new DataPoint(210, 156.322715249645));
            lineSeries1.Points.Add(new DataPoint(212.5, 158.220287512602));
            lineSeries1.Points.Add(new DataPoint(215, 160.239688355634));
            lineSeries1.Points.Add(new DataPoint(217.5, 162.161841185327));
            lineSeries1.Points.Add(new DataPoint(220, 164.135674956621));
            lineSeries1.Points.Add(new DataPoint(222.5, 166.004863421039));
            lineSeries1.Points.Add(new DataPoint(225, 167.640597222485));
            lineSeries1.Points.Add(new DataPoint(227.5, 168.928940251669));
            lineSeries1.Points.Add(new DataPoint(230, 170.552868003446));
            lineSeries1.Points.Add(new DataPoint(232.5, 172.399869402056));
            lineSeries1.Points.Add(new DataPoint(235, 174.346163347851));
            lineSeries1.Points.Add(new DataPoint(237.5, 176.33268720905));
            lineSeries1.Points.Add(new DataPoint(240, 178.153302327545));
            lineSeries1.Points.Add(new DataPoint(242.5, 180.100497714128));
            lineSeries1.Points.Add(new DataPoint(245, 181.831839179449));
            lineSeries1.Points.Add(new DataPoint(247.5, 183.611763662664));
            lineSeries1.Title = "Average";
            lineAndAreaSeriesModel.Series.Add(lineSeries1);
        }

        private void InitializeLineSeriesModel()
        {

            // http://en.wikipedia.org/wiki/Normal_distribution

            lineSeriesModel = new PlotModel
            {
                //Title = "Normal distribution",
                //Subtitle = "Probability density function"
            };

            lineSeriesModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = -0.05,
                Maximum = 1.05,
                MajorStep = 0.2,
                MinorStep = 0.05,
                TickStyle = TickStyle.Inside
            });
            lineSeriesModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = -5.25,
                Maximum = 5.25,
                MajorStep = 1,
                MinorStep = 0.25,
                TickStyle = TickStyle.Inside
            });
            lineSeriesModel.Series.Add(CreateNormalDistributionSeries(-5, 5, 0, 0.2));
            lineSeriesModel.Series.Add(CreateNormalDistributionSeries(-5, 5, 0, 1));
            lineSeriesModel.Series.Add(CreateNormalDistributionSeries(-5, 5, 0, 5));
            lineSeriesModel.Series.Add(CreateNormalDistributionSeries(-5, 5, -2, 0.5));

            lineSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;
        }

        private void InitializeMultiValueAxesBarSeriesModel()
        {
            multiValueAxesBarSeriesModel = new PlotModel(); // { Title = "Multiple Value Axes" };

            multiValueAxesBarSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;

            multiValueAxesBarSeriesModel.DefaultColors = OxyPalettes.Viridis(4).Colors;

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };
            var valueAxis1 = new LinearAxis { /* Title = "Value Axis 1", */ Position = AxisPosition.Bottom, MinimumPadding = 0.06, MaximumPadding = 0.06, ExtraGridlines = new[] { 0d }, EndPosition = .5, Key = "x1" };
            var valueAxis2 = new LinearAxis { /* Title = "Value Axis 2", */ Position = AxisPosition.Bottom, MinimumPadding = 0.06, MaximumPadding = 0.06, ExtraGridlines = new[] { 0d }, StartPosition = .5, Key = "x2" };
            multiValueAxesBarSeriesModel.Axes.Add(categoryAxis);
            multiValueAxesBarSeriesModel.Axes.Add(valueAxis1);
            multiValueAxesBarSeriesModel.Axes.Add(valueAxis2);

            var series = new List<BarSeries>
            {
                new BarSeries { XAxisKey = "x1" },
                new BarSeries { XAxisKey = "x1" },
                new BarSeries { XAxisKey = "x2" },
                new BarSeries { XAxisKey = "x2" },
            };

            var rnd = new Random(1);
            foreach (var s in series)
            {
                for (var i = 0; i < 4; i++)
                {
                    s.Items.Add(new BarItem() { Value = rnd.Next(-100, 100) });
                }

                multiValueAxesBarSeriesModel.Series.Add(s);
            }
        }

        private void InitializePieSeriesModel()
        {
            pieSeriesModel = new PlotModel(); // { Title = "Pie Sample1" };

            pieSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;

            dynamic seriesP1 = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };

            seriesP1.Slices.Add(new PieSlice("Africa", 1030) { IsExploded = false, Fill = OxyColors.PaleVioletRed });
            seriesP1.Slices.Add(new PieSlice("Americas", 929) { IsExploded = true });
            seriesP1.Slices.Add(new PieSlice("Asia", 4157) { IsExploded = true });
            seriesP1.Slices.Add(new PieSlice("Europe", 739) { IsExploded = true });
            seriesP1.Slices.Add(new PieSlice("Oceania", 35) { IsExploded = true });

            pieSeriesModel.Series.Add(seriesP1);
        }

        private void InitializeRectangularHeatmapSeriesModel()
        {
            rectangularHeatMapSeriesModel = new PlotModel(); // { Title = "Hmmmm ... cookies" };

            rectangularHeatMapSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;

            // Weekday axis (horizontal)
            rectangularHeatMapSeriesModel.Axes.Add(new CategoryAxis
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
            rectangularHeatMapSeriesModel.Axes.Add(new CategoryAxis
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
            rectangularHeatMapSeriesModel.Axes.Add(new LinearColorAxis
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

            rectangularHeatMapSeriesModel.Series.Add(heatMapSeries);
        }

        private void InitializeStemSeriesModel()
        {
            stemSeriesModel = new PlotModel(); // { Title = "Trigonometric functions" };

            stemSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;

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

            stemSeriesModel.Series.Add(sinStemSeries);
        }

        private void InitializeTwoColorAreaSeriesModel()
        {
            twoColorAreaSeriesModel = new PlotModel(); // { Title = "TwoColorAreaSeries" };

            twoColorAreaSeriesModel.PlotAreaBorderThickness = new OxyThickness(0);

            //var l = new Legend
            //{
            //    LegendSymbolLength = 24
            //};

            //model.Legends.Add(l);

            var s1 = new TwoColorAreaSeries
            {
                // Title = "Temperature at Eidesmoen, December 1986.",
                TrackerFormatString = "December {2:0}: {4:0.0} °C",
                Color = OxyColors.Black,
                Color2 = OxyColors.Brown,
                MarkerFill = OxyColors.Red,
                Fill = OxyColors.Tomato,
                Fill2 = OxyColors.LightBlue,
                MarkerFill2 = OxyColors.Blue,
                MarkerStroke = OxyColors.Brown,
                MarkerStroke2 = OxyColors.Black,
                StrokeThickness = 2,
                Limit = 0,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
            };

            var temperatures = new[] { 5, 0, 7, 7, 4, 3, 5, 5, 11, 4, 2, 3, 2, 1, 0, 2, -1, 0, 0, -3, -6, -13, -10, -10, 0, -4, -5, -4, 3, 0, -5 };

            for (int i = 0; i < temperatures.Length; i++)
            {
                s1.Points.Add(new DataPoint(i + 1, temperatures[i]));
            }

            twoColorAreaSeriesModel.Series.Add(s1);
            twoColorAreaSeriesModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Temperature", Unit = "°C", ExtraGridlines = new[] { 0.0 } });
            twoColorAreaSeriesModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Date" });
        }
    }
}
