#nullable enable

using System;
namespace IBL.BO
{
    public class Drone
    {
        int Id { get; init; }
        string Model { get; init; }
        IDAL.DO.WeightCategory WeightCategory { get; init; }
        double BatteryStatus { get; init; }
        DroneStatus Status { get; init; }
        PackageInTransfer? Package { get; init; }
        Location Location { get; init; }

        public Drone(int id, string model, IDAL.DO.WeightCategory weightCategory, double batteryStatus, DroneStatus status, PackageInTransfer? package, Location location)
        {
            Id = id;
            Model = model;
            WeightCategory = weightCategory;
            BatteryStatus = batteryStatus;
            Status = status;
            Package = package;
            Location = location;
        }
    }
}
