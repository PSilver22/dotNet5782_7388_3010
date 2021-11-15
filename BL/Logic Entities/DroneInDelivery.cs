using System;
namespace IBL.BO
{
    public class DroneInDelivery
    {
        int Id { get; init; }
        double BatteryStatus { get; init; }
        Location CurentLocation { get; init; }

        public DroneInDelivery(int id, double batteryStatus, Location curentLocation)
        {
            Id = id;
            BatteryStatus = batteryStatus;
            CurentLocation = curentLocation;
        }
    }
}
