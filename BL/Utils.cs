﻿using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;
using System.Device.Location;
namespace IBL
{
    internal class Utils
    {
        /// <summary>
        /// Calculates the distance between two locations using Haversine formula
        /// </summary>
        /// <param name="l1">the first location</param>
        /// <param name="l2">the second location</param>
        /// <returns>the distance between the two locations</returns>
        public static double DistanceBetween(Location l1, Location l2)
        {
            return new GeoCoordinate(l1.Latitude, l1.Longitude).GetDistanceTo(new(l2.Latitude, l2.Longitude));
        }

        /// <summary>
        /// Finds the base station closest to a given location
        /// </summary>
        /// <param name="location">the location</param>
        /// <param name="stations">the stations to search through</param>
        /// <returns></returns>
        public static IDAL.DO.Station ClosestStation(Location location, IEnumerable<IDAL.DO.Station> stations)
        {
            return stations.Select<IDAL.DO.Station, (IDAL.DO.Station station, double dist)>(s =>
            {
                Location sLoc = new(s.Latitude, s.Longitude);
                return (s, Utils.DistanceBetween(location, sLoc));
            }).Aggregate((curr, next) => curr.dist < next.dist ? curr : next).station;
        }
    }
}
