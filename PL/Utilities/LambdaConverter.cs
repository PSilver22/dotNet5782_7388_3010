#nullable enable

using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.Utilities
{
    public class LambdaConverter<T>: IValueConverter
    {
        public Func<object?, T> Func { get; set; }

        public LambdaConverter(Func<object?, T> func)
        {
            Func = func;
        }

        public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            return Func(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}