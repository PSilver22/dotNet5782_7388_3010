#nullable enable

using System;
namespace IBL.BO
{
    public class DroneListing
    {
        public int Id { get; init; }
        public string Model { get; init; }
        public IDAL.DO.WeightCategory WeightCategory { get; init; }
        public double BatteryStatus { get; init; }
        public DroneStatus Status { get; init; }
        public Location Location { get; init; }
        public int? PackageId { get; init; }

        public DroneListing(int id, string model, IDAL.DO.WeightCategory weightCategory, double batteryStatus, DroneStatus status, Location location, int? packageId)
        {
            Id = id;
            Model = model;
            WeightCategory = weightCategory;
            BatteryStatus = batteryStatus;
            Status = status;
            Location = location;
            PackageId = packageId;
        }

        public override string ToString()
        {
            return $"id: {Id}\n" +
                $"model: {Model}\n" +
                $"weight category: {WeightCategory}\n" +
                $"battery status: {BatteryStatus * 100:F2}\n" +
                $"status: {Status}\n" +
                $"location: {Location}" +
                PackageId is not null ? $"\npackage: {PackageId!}" : "";
        }
    }
}
