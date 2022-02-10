using System;
namespace BL
{
    public class Location
    {
        public double Latitude { get; init; }
        public double Longitude { get; init; }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static bool operator==(Location location1, Location location2) {
            return location1.Latitude == location2.Latitude && 
                location1.Longitude == location2.Longitude;
        }
        public static bool operator !=(Location location1, Location location2) {
            return !(location1 == location2);
        }

        public override string ToString()
        {
            return FormatUtils.Coordinates.Sexagesimal(Latitude, FormatUtils.Coordinates.Axis.latitude) +
                ", " + FormatUtils.Coordinates.Sexagesimal(Longitude, FormatUtils.Coordinates.Axis.longitude);
        }
    }
}
