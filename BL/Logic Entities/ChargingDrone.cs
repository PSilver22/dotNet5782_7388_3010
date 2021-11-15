using System;
namespace IBL.BO
{
    public class ChargingDrone
    {
        int Id { get; init; }
        double BatteryStatus { get; init; }

        public ChargingDrone(int id, double batteryStatus)
        {
            Id = id;
            BatteryStatus = batteryStatus;
        }
    }
}
