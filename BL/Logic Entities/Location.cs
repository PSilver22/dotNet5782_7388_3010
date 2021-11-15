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
    }
}
