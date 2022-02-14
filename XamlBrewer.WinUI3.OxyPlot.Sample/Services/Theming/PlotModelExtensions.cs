using Microsoft.UI.Xaml;
using OxyPlot;

namespace XamlBrewer.WinUI3.Services.Theming
{
    internal static class PlotModelExtensions
    {
        public static void ApplyTheme(this PlotModel plotModel, ElementTheme theme)
        {
            // Beware: Do not use OxyColors.Black and OxyColors.White.
            // Their cached brushes are reversed, based on the Theme. Confusing!

            var foreground = theme == ElementTheme.Light ? OxyColor.FromRgb(32, 32, 32) : OxyColors.WhiteSmoke;

            if (plotModel.TextColor != OxyColors.Transparent)
            {
                plotModel.TextColor = foreground;
            }

            foreach (var axis in plotModel.Axes)
            {
                if (axis.TicklineColor != OxyColors.Transparent)
                {
                    axis.TicklineColor = foreground;
                }
                if (axis.AxislineColor != OxyColors.Transparent)
                {
                    axis.AxislineColor = foreground;
                }
            }

            plotModel.InvalidatePlot(false);
        }
    }
}
