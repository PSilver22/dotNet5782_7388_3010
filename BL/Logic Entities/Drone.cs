#nullable enable

using System;
namespace IBL.BO
{
    public class Drone
    {
        public int Id { get; init; }
        public string Model { get; init; }
        public IDAL.DO.WeightCategory WeightCategory { get; init; }
        public double BatteryStatus { get; init; }
        public DroneStatus Status { get; init; }
        public PackageInTransfer? Package { get; init; }
        public Location Location { get; init; }

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

        public override string ToString()
        {
            return $"id: {Id}\n" +
                $"model: {Model}\n" +
                $"weight category: {WeightCategory}\n" +
                $"battery status: {BatteryStatus:F2}\n" +
                $"status: {Status}\n" +
                (Package is not null ? $"package:\n{Package!}\n" : "") +
                $"location: {Location}";
        }
    }
}
