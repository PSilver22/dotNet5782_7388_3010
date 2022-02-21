using System;
using System.Globalization;
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
            return values[0] switch
            {
                BaseStationListing sl => bl.GetBaseStation(sl.Id),
                CustomerListing cl => bl.GetCustomer(cl.Id),
                DroneListing dl => bl.GetDrone(dl.Id),
                _ => throw new InvalidOperationException()
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}