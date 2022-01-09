using System;
namespace BL
{
    // BUG: Battery status is wrong
    public class ChargingDrone
    {
        public int Id { get; init; }
        public double BatteryStatus { get; init; }

        public ChargingDrone(int id, double batteryStatus)
        {
            Id = id;
            BatteryStatus = batteryStatus;
        }

        public override string ToString()
        {
            return $"  id: {Id}\n" +
                $"  battery status: {BatteryStatus:F2}";
        }
    }
}
