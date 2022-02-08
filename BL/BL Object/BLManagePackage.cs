#nullable enable

using System;
using System.Linq;
using BL;

namespace BlApi
{
    public partial class BL : IBL
    {
        public void AssignPackageToDrone(int id)
        {
            var drone = GetDrone(id);

            if (drone.Status != DroneStatus.free)
            {
                throw new DroneNotFreeException("assign package");
            }

            var package = dal.GetPackageList()
                .Where(p => p.DroneId is null)
                .Where(p => p.Weight <= drone.WeightCategory)
                .OrderByDescending(p => p.Priority)
                .ThenByDescending(p =>
                {
                    var sender = dal.GetCustomer(p.SenderId);
                    return Utils.DistanceBetween(drone.Location, new(sender.Latitude, sender.Longitude));
                })
                .ThenByDescending(p => p.Weight)
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

                    var reqBattery = powerConsumption.Free * (distToSender + distToStation) +
                                     (GetPowerConsumption(p.Weight) * distToRecip);

                    return drone.BatteryStatus >= reqBattery;
                })
                .FirstOrDefault();

            if (package.Requested != default)
            {
                dal.UpdatePackage(package.Id, droneId: drone.Id, scheduled: DateTime.UtcNow);
            }
            else
            {
                throw new NoRelevantPackageException();
            }
        }

        public void CollectPackageByDrone(int id)
        {
            var drone = GetDrone(id);

            if (drone.Package is null)
            {
                throw new DroneNotAssignedPackageException();
            }

            var package = dal.GetPackage(drone.Package.Id);

            if (package.PickedUp is not null)
            {
                throw new PackageAlreadyPickedUpException();
            }

            var sender = dal.GetCustomer(package.SenderId);
            Location senderLoc = new(sender.Latitude, sender.Longitude);
            var batteryUsage = powerConsumption.Free * Utils.DistanceBetween(drone.Location, senderLoc);

            dal.UpdateDrone(id,
                battery: drone.BatteryStatus - batteryUsage,
                longitude: sender.Longitude,
                latitude: sender.Latitude);

            dal.UpdatePackage(package.Id, pickedUp: DateTime.UtcNow);
        }

        public void DeliverPackageByDrone(int id)
        {
            var drone = GetDrone(id);

            if (drone.Package is null)
            {
                throw new DroneNotAssignedPackageException();
            }

            var package = dal.GetPackage(drone.Package.Id);

            if (package.PickedUp is null)
            {
                throw new PackageAlreadyPickedUpException();
            }

            var recip = dal.GetCustomer(package.TargetId);
            Location recipLoc = new(recip.Latitude, recip.Longitude);

            dal.UpdateDrone(id,
                battery: drone.BatteryStatus - GetPowerConsumption(package.Weight) *
                Utils.DistanceBetween(drone.Location, recipLoc),
                longitude: recip.Longitude,
                latitude: recip.Latitude);

            dal.UpdatePackage(package.Id, delivered: DateTime.UtcNow);
        }
    }
}