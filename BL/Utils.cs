using System;
using IBL.BO;
using System.Device.Location;
namespace IBL
{
    internal class Utils
    {

        public static double DistanceBetween(Location l1, Location l2)
        {
            return new GeoCoordinate(l1.Latitude, l1.Longitude).GetDistanceTo(new(l2.Latitude, l2.Longitude));
        }
    }
}
