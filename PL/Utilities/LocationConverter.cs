using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.Utilities
{
    public class LocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                MapControl.Location loc => new BL.Location(loc.Latitude, loc.Longitude),
                BL.Location loc => new MapControl.Location(loc.Latitude, loc.Longitude),
                _ => null
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                MapControl.Location loc => new BL.Location(loc.Latitude, loc.Longitude),
                BL.Location loc => new MapControl.Location(loc.Latitude, loc.Longitude),
                _ => null
            };
        }
    }
}