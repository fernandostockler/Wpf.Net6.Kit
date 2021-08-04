using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Wpf.Net6.Kit.Controls.Shared
{
    [ValueConversion(typeof(double), typeof(GridLength))]
    public class DoubleToGridlenghtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (value is double height) ? new GridLength(height) : value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}