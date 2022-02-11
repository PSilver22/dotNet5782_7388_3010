using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace PL.Utilities
{
    public class InCollectionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || values[1] is not IEnumerable)
                throw new InvalidOperationException();
            return ((IEnumerable) values[1]).Cast<object>().Contains(values[0]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}