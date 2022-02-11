using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using BL;
using BlApi;

namespace PL
{
    public class ListingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || values[1] is not IBL)
                throw new InvalidOperationException();
            var bl = (IBL) values[1];
            switch (values[0])
            {
                case BaseStationListing sl:
                    var s = bl.GetBaseStation(sl.Id);
                    return new MapControl.Location(s.Location.Latitude, s.Location.Longitude);
                case CustomerListing cl:
                    var c = bl.GetCustomer(cl.Id);
                    return new MapControl.Location(c.Location.Latitude, c.Location.Longitude);
                case DroneListing dl:
                    var d = bl.GetDrone(dl.Id);
                    return new MapControl.Location(d.Location.Latitude, d.Location.Longitude);
                default:
                    throw new InvalidOperationException();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}