using System;
using System.Linq;
using System.Text;

namespace SGVChart
{
    public class SVGCreator
    {
        public static string CreateSVG(string svgTemplate, double[] values, string[] labels)
        {
            var paths = GenerateSVGPieChart(values, labels);
            return string.Format(svgTemplate, paths);
        }
        private static (double X, double Y) GetCoordinatesForPercent(double percent)
        {
            var x = Math.Cos(2 * Math.PI * percent);
            var y = Math.Sin(2 * Math.PI * percent);
            return (x, y);
        }
        private static string GenerateSVGPieChart(double[] values, string[] labels)
        {
            var sb = new StringBuilder();
            var cumulativePercent = 0D;

            foreach (var slice in labels.Zip(
                values, (label, value) => new { Color = label, Prercent = value }))
            {
                // destructing assignment sets the two variables at once
                var (startX, startY) = GetCoordinatesForPercent(cumulativePercent);

                // each slice starts where the last slice ended, so keep a cumulative percent
                cumulativePercent += slice.Prercent;
                var (endX, endY) = GetCoordinatesForPercent(cumulativePercent);

                // if the slice is more than 50%, take the large arc (the long way around)
                var largeArcFlag = slice.Prercent > .5 ? 1 : 0;

                // create an array and join it just for code readability
                var pathData = string.Join(" ", new[] {
                    $"M {startX} {startY}",// Move
                    $"A 1 1 0 {largeArcFlag} 1 {endX} {endY}",// Arc
                    "L 0 0" // Line
                });

                sb.Append($"<path d='{pathData}' fill='{slice.Color}'></path>");
            }
            return sb.ToString();
        }
    }
}
