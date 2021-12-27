using System;
namespace BL
{
    public class DroneInDelivery
    {
        public int Id { get; init; }
        public double BatteryStatus { get; init; }
        public Location CurrentLocation { get; init; }

        public DroneInDelivery(int id, double batteryStatus, Location currentLocation)
        {
            Id = id;
            BatteryStatus = batteryStatus;
            CurrentLocation = currentLocation;
        }

        public override string ToString()
        {
            return $"  id: {Id}\n" +
                $"  battery status: {BatteryStatus:F2}\n" +
                $"  location: {CurrentLocation}";
        }
    }
}
