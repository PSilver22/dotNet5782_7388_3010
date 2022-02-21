using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.Utilities
{
    public class ScaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double v && parameter is double k)
                    return v * k;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double v && parameter is double k)
                return v / k;
            return 0;
        }
    }
}