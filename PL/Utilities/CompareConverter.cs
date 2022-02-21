using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.Utilities
{
    public class CompareConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IComparable v && parameter is IComparable p)
                return v.CompareTo(p) switch
                {
                    < 0 => -1,
                    > 0 => 1,
                    _ => 0
                };

            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}