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
            { throw new DroneNotFoundException(id); }
            var drone = drones[droneIndex];

            if (drone.Status != DroneStatus.free)
            { throw new DroneNotFreeException("assign package"); }

            IDAL.DO.Package package = dal.GetPackageList()
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

                    var reqBattery = powerConsumption.Free * (distToSender + distToStation) + (GetPowerConsumption(p.Weight) * distToRecip);

                    Console.WriteLine(reqBattery);

                    return drone.BatteryStatus >= reqBattery;
                })
                .FirstOrDefault();

            if (package.Requested != default)
            {
                drone.Status = DroneStatus.delivering;
                drone.PackageId = package.Id;

                drones[droneIndex] = drone;

                dal.UpdatePackage(package.Id, droneId: drone.Id, scheduled: DateTime.UtcNow);
            }
            else
            {
                throw new NoRelevantPackageException();
            }
        }

        public void CollectPackageByDrone(int id)
        {
            var droneIndex = drones.FindIndex(d => d.Id == id);
            if (droneIndex == -1)
            { throw new DroneNotFoundException(id); }
            var drone = drones[droneIndex];

            if (drone.PackageId is null)
            { throw new DroneNotAssignedPackageException(); }

            var package = dal.GetPackage(drone.PackageId!.Value);

            if (package.PickedUp is not null)
            { throw new PackageAlreadyPickedUpException(); }

            var sender = dal.GetCustomer(package.SenderId);
            Location senderLoc = new(sender.Latitude, sender.Longitude);
            var batteryUsage = powerConsumption.Free * Utils.DistanceBetween(drone.Location, senderLoc);

            drone.BatteryStatus -= batteryUsage;
            drone.Location = senderLoc;
            drones[droneIndex] = drone;

            dal.UpdateDrone(id, battery: drone.BatteryStatus);

            dal.UpdatePackage(package.Id, pickedUp: DateTime.UtcNow);
        }

        public void DeliverPackageByDrone(int id)
        {
            var droneIndex = drones.FindIndex(d => d.Id == id);
            if (droneIndex == -1)
            { throw new DroneNotFoundException(id); }
            var drone = drones[droneIndex];

            if (drone.PackageId is null)
            { throw new DroneNotAssignedPackageException(); }

            var package = dal.GetPackage(drone.PackageId!.Value);

            if (package.PickedUp is null)
            { throw new PackageAlreadyPickedUpException(); }

            var recip = dal.GetCustomer(package.TargetId);
            Location recipLoc = new(recip.Latitude, recip.Longitude);
            drone.BatteryStatus -= GetPowerConsumption(package.Weight) * Utils.DistanceBetween(drone.Location, recipLoc);
            drone.Location = recipLoc;
            drone.Status = DroneStatus.free;
            drone.PackageId = null;
            drones[droneIndex] = drone;

            dal.UpdateDrone(id, battery: drone.BatteryStatus);

            dal.UpdatePackage(package.Id, delivered: DateTime.UtcNow);
        }
    }
}
