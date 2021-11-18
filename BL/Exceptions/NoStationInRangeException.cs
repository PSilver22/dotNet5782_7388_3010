using System;

namespace IBL
{
    class NoStationInRangeException : Exception
    {
        public NoStationInRangeException() : base($"No station with available charging slot within flying range") { }

        public NoStationInRangeException(double maxDistance) : base($"No station with available charging slot within flying range (max: {maxDistance:F1} km)") { }
    }
}
