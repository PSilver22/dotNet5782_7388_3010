#nullable enable

using System;
namespace BL
{
    public class DroneListing
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public DO.WeightCategory WeightCategory { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatus Status { get; set; }
        public Location Location { get; set; }
        public int? PackageId { get; set; }
        
        public int? ChargingStationId { get; set; }

        public DroneListing(int id, string model, DO.WeightCategory weightCategory, double batteryStatus, DroneStatus status, Location location, int? packageId, int? chargingStationId)
        {
            Id = id;
            Model = model;
            WeightCategory = weightCategory;
            BatteryStatus = batteryStatus;
            Status = status;
            Location = location;
            PackageId = packageId;
            ChargingStationId = chargingStationId;
        }

        public override string ToString()
        {
            return $"id: {Id}\n" +
                $"model: {Model}\n" +
                $"weight category: {WeightCategory}\n" +
                $"battery status: {BatteryStatus:F2}\n" +
                $"status: {Status}\n" +
                $"location: {Location}\n" +
                (PackageId is not null ? $"\npackage: {PackageId}" : "");
        }
    }
}
