using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Wpf.Net6.Kit.Controls.Shared
{
    public class BackgroundToForegroundConverter : IValueConverter, IMultiValueConverter
    {
        private static BackgroundToForegroundConverter? _instance;

        // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
        static BackgroundToForegroundConverter() { }

        private BackgroundToForegroundConverter() { }

        public static BackgroundToForegroundConverter Instance => _instance ??= new BackgroundToForegroundConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush && brush != null)
            {
                Color idealForegroundColor = IdealTextColor(brush.Color);
                SolidColorBrush? foreGroundBrush = new(idealForegroundColor);
                foreGroundBrush.Freeze();
                return foreGroundBrush;
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Brush? bgBrush = values.Length > 0 ? values[0] as Brush : null;
            Brush? titleBrush = values.Length > 1 ? values[1] as Brush : null;
#pragma warning disable CS8604 // Possível argumento de referência nula.
            /// <remarks> bgBrush may be null however the converter always returns a non-null Brush. </remarks>
            return titleBrush ?? Convert(bgBrush, targetType, parameter, culture);
#pragma warning restore CS8604 // Possível argumento de referência nula.
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return targetTypes.Select(t => DependencyProperty.UnsetValue).ToArray();
        }

        /// <summary>
        /// Determining Ideal Text Color Based on Specified Background Color
        /// http://www.codeproject.com/KB/GDI-plus/IdealTextColor.aspx
        /// </summary>
        /// <param name = "bg">The bg.</param>
        /// <returns></returns>
        private static Color IdealTextColor(Color bg)
        {
            const int nThreshold = 86; //105;
            int bgDelta = System.Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) + (bg.B * 0.114));
            Color foreColor = (255 - bgDelta < nThreshold) ? Colors.Black : Colors.White;
            return foreColor;
        }
    }
}