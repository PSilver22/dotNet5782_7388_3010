using System;
namespace IBL.BO
{
    public class Location
    {
        double Latitude { get; init; }
        double Longitude { get; init; }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return FormatUtils.Coordinates.Sexagesimal(Latitude, FormatUtils.Coordinates.Axis.latitude) +
                ", " + FormatUtils.Coordinates.Sexagesimal(Longitude, FormatUtils.Coordinates.Axis.longitude);
        }
    }
}
