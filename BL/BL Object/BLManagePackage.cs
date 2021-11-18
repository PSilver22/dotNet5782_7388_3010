#nullable enable

using System;
using System.Linq;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        public void AssignPackageToDrone(int id)
        {
            var droneIndex = drones.FindIndex(d => d.Id == id);
            if (droneIndex == -1)
            { /* TODO: throw DroneNotFound */ }
            var drone = drones[droneIndex];

            if (drone.Status != DroneStatus.free)
            { /* TODO: throw DroneNotFree */ }

            IDAL.DO.Package? package = dal.GetPackageList()
                .Where(p => p.DroneId is null)
                .Where(p => p.Weight <= drone.WeightCategory)
                .OrderByDescending(p => p.Weight)
                .OrderByDescending(p => p.Priority)
                .OrderBy(p =>
                {
                    var sender = dal.GetCustomer(p.SenderId);
                    return Utils.DistanceBetween(drone.Location, new(sender.Latitude, sender.Longitude));
                })
                .Where(p =>
                {
                    var sender = dal.GetCustomer(p.SenderId);
                    Location senderLoc = new(sender.Latitude, sender.Longitude);
                    var recip = dal.GetCustomer(p.TargetId);
                    Location recipLoc = new(recip.Latitude, recip.Longitude);
                    var station = Utils.ClosestStation(recipLoc, dal.GetStationList());

                    var distToSender = Utils.DistanceBetween(drone.Location, senderLoc);
                    var distToRecip = Utils.DistanceBetween(senderLoc, recipLoc);
                    var distToStation = Utils.DistanceBetween(recipLoc, new(station.Latitude, station.Longitude));

                    var reqBattery = powerConsumption.Free * (distToSender + distToStation) + (getPowerConsumption(p.Weight) * distToRecip);

                    return drone.BatteryStatus >= reqBattery;
                })
                .FirstOrDefault();

            if (package is not null)
            {
                drone.Status = DroneStatus.delivering;
                drone.PackageId = package?.Id;

                drones[droneIndex] = drone;

                dal.UpdatePackage(package!.Value.Id, droneId: drone.Id, scheduled: DateTime.UtcNow);
            }
            else
            {
                // TODO: throw NoRelevantPackage
            }
        }

        public void CollectPackageByDrone(int id)
        {
            var droneIndex = drones.FindIndex(d => d.Id == id);
            if (droneIndex == -1)
            { /* TODO: throw DroneNotFound */ }
            var drone = drones[droneIndex];

            if (drone.PackageId is null)
            { /* TODO: throw DroneNotAssignedPackage */ }

            var package = dal.GetPackage(drone.PackageId!.Value);

            if (package.PickedUp is not null)
            { /* TODO: throw PackageAlreadyPickedUp */ }

            var sender = dal.GetCustomer(package.SenderId);
            Location senderLoc = new(sender.Latitude, sender.Longitude);
            drone.BatteryStatus -= powerConsumption.Free * Utils.DistanceBetween(drone.Location, senderLoc);
            drone.Location = senderLoc;
            drones[droneIndex] = drone;

            dal.UpdateDrone(id, battery: drone.BatteryStatus);

            dal.UpdatePackage(package.Id, pickedUp: DateTime.UtcNow);
        }

        public void DeliverPackageByDrone(int id)
        {
            var droneIndex = drones.FindIndex(d => d.Id == id);
            if (droneIndex == -1)
            { /* TODO: throw DroneNotFound */ }
            var drone = drones[droneIndex];

            if (drone.PackageId is null)
            { /* TODO: throw DroneNotAssignedPackage */ }

            var package = dal.GetPackage(drone.PackageId!.Value);

            if (package.PickedUp is null)
            { /* TODO: throw PackageNotPickedUp */ }

            if (package.Delivered is not null)
            { /* TODO: throw PackageAlreadyDelivered */ }

            var recip = dal.GetCustomer(package.TargetId);
            Location recipLoc = new(recip.Latitude, recip.Longitude);
            drone.BatteryStatus -= getPowerConsumption(package.Weight) * Utils.DistanceBetween(drone.Location, recipLoc);
            drone.Location = recipLoc;
            drone.Status = DroneStatus.free;
            drones[droneIndex] = drone;

            dal.UpdateDrone(id, battery: drone.BatteryStatus);

            dal.UpdatePackage(package.Id, delivered: DateTime.UtcNow);
        }
    }
}
