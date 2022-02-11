#nullable enable

using System;
using System.Linq;
using BL;
using System.Runtime.CompilerServices;

namespace BlApi
{
    public partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssignPackageToDrone(int id)
        {
            lock (Dal) {
                var drone = GetDrone(id);

                if (drone.Status != DroneStatus.free) {
                    throw new DroneNotFreeException("assign package");
                }

                var package = Dal.GetPackageList()
                    .Where(p => p.DroneId is null)
                    .Where(p => p.Weight <= drone.WeightCategory)
                    .OrderByDescending(p => p.Priority)
                    .ThenByDescending(p => {
                        var sender = Dal.GetCustomer(p.SenderId);
                        return Utils.DistanceBetween(drone.Location, new(sender.Latitude, sender.Longitude));
                    })
                    .ThenByDescending(p => p.Weight)
                    .Where(p => {
                        var sender = Dal.GetCustomer(p.SenderId);
                        Location senderLoc = new(sender.Latitude, sender.Longitude);
                        var recip = Dal.GetCustomer(p.TargetId);
                        Location recipLoc = new(recip.Latitude, recip.Longitude);
                        var station = Utils.ClosestStation(recipLoc, Dal.GetStationList());

                        var distToSender = Utils.DistanceBetween(drone.Location, senderLoc);
                        var distToRecip = Utils.DistanceBetween(senderLoc, recipLoc);
                        var distToStation = Utils.DistanceBetween(recipLoc, new(station.Latitude, station.Longitude));

                        var reqBattery = PowerConsumption.Free * (distToSender + distToStation) +
                                        (GetPowerConsumption(p.Weight) * distToRecip);

                        return drone.BatteryStatus >= reqBattery;
                    })
                    .FirstOrDefault();

                if (package.Requested != default) {
                    Dal.UpdatePackage(package.Id, droneId: drone.Id, scheduled: DateTime.UtcNow);
                } else {
                    throw new NoRelevantPackageException();
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CollectPackageByDrone(int id)
        {
            lock (Dal) {
                var drone = GetDrone(id);

                if (drone.Package is null) {
                    throw new DroneNotAssignedPackageException();
                }

                var package = Dal.GetPackage(drone.Package.Id);

                if (package.PickedUp is not null) {
                    throw new PackageAlreadyPickedUpException();
                }

                var sender = Dal.GetCustomer(package.SenderId);
                Location senderLoc = new(sender.Latitude, sender.Longitude);
                var batteryUsage = PowerConsumption.Free * Utils.DistanceBetween(drone.Location, senderLoc);

                Dal.UpdateDrone(id,
                    battery: drone.BatteryStatus - batteryUsage,
                    longitude: sender.Longitude,
                    latitude: sender.Latitude);

                Dal.UpdatePackage(package.Id, pickedUp: DateTime.UtcNow);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliverPackageByDrone(int id) {
            lock (Dal) {
                var drone = GetDrone(id);

                if (drone.Package is null) {
                    throw new DroneNotAssignedPackageException();
                }

                var package = Dal.GetPackage(drone.Package.Id);

                if (package.PickedUp is null) {
                    throw new PackageAlreadyPickedUpException();
                }

                var recip = Dal.GetCustomer(package.TargetId);
                Location recipLoc = new(recip.Latitude, recip.Longitude);

                Dal.UpdateDrone(id,
                    battery: drone.BatteryStatus - GetPowerConsumption(package.Weight) *
                    Utils.DistanceBetween(drone.Location, recipLoc),
                    longitude: recip.Longitude,
                    latitude: recip.Latitude);

                Dal.UpdatePackage(package.Id, delivered: DateTime.UtcNow);
            }
        }
    }
}