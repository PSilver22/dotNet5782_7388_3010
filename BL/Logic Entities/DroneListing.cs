#nullable enable

using System;
namespace IBL.BO
{
    public class DroneListing
    {
        int Id { get; init; }
        string Model { get; init; }
        IDAL.DO.WeightCategory WeightCategory { get; init; }
        double BatteryStatus { get; init; }
        DroneStatus Status { get; init; }
        Location Location { get; init; }
        int? PackageId { get; init; }

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
    }
}
