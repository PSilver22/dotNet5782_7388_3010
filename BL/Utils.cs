using System.Collections.Generic;
using System.Linq;
using BL;
using System.Device.Location;
namespace BlApi
{
    internal class Utils
    {
        /// <summary>
        /// Calculates the distance between two locations using Haversine formula
        /// </summary>
        /// <param name="l1">the first location</param>
        /// <param name="l2">the second location</param>
        /// <returns>the distance between the two locations in km</returns>
        public static double DistanceBetween(Location l1, Location l2)
        {
            return new GeoCoordinate(l1.Latitude, l1.Longitude).GetDistanceTo(new(l2.Latitude, l2.Longitude)) / 1000;
        }


        /// <summary>
        /// Finds the base station closest to a given location
        /// </summary>
        /// <param name="location">the location</param>
        /// <param name="stations">the stations to search through</param>
        /// <returns></returns>
        public static DO.Station ClosestStation(Location location, IEnumerable<DO.Station> stations)
        {
            return stations.Select<DO.Station, (DO.Station station, double dist)>(s =>
            {
                Location sLoc = new(s.Latitude, s.Longitude);
                return (s, Utils.DistanceBetween(location, sLoc));
            }).Aggregate((curr, next) => curr.dist < next.dist ? curr : next).station;
        }

        public static double GetBatteryUsage(Drone drone, Location fromLocation, Location toLocation) {
            return BL.Instance.GetPowerConsumption(drone.WeightCategory) * DistanceBetween(fromLocation, toLocation);
        }
    }
}