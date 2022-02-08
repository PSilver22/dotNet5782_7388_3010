#nullable enable

using System;
using System.Linq;
using BL;
using DalApi;
using System.Runtime.CompilerServices;

namespace BlApi
{
    public partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation GetBaseStation(int id)
        {
            lock (dal) {
                try {
                    var dalStation = dal.GetStation(id);
                    var chargingDrones = dal.GetDroneChargeList()
                        .Where(cd => cd.StationId == id)
                        .Select(cd => new ChargingDrone(cd.DroneId, dal.GetDrone(cd.DroneId).Battery)).ToList();
                    return new(id, dalStation.Name, new(dalStation.Latitude, dalStation.Longitude), dalStation.ChargeSlots,
                        chargingDrones);
                } catch (IdNotFoundException) {
                    throw new StationNotFoundException(id);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            lock (dal) {
                try {
                    var dalCustomer = dal.GetCustomer(id);
                    var packages = dal.GetPackageList();
                    var sentPackages = packages
                        .Where(p => p.SenderId == id)
                        .Select(p => {
                            var status =
                                p.Delivered is not null ? PackageStatus.delivered
                                : p.PickedUp is not null ? PackageStatus.collected
                                : p.Scheduled is not null ? PackageStatus.assigned
                                : PackageStatus.created;
                            var dalRecip = dal.GetCustomer(p.TargetId);
                            var recipient = new PackageCustomer(dalRecip.Id, dalRecip.Name);
                            return new PackageInCustomer(p.Id, p.Weight, p.Priority, status, recipient);
                        });
                    var receivingPackages = packages
                        .Where(p => p.TargetId == id)
                        .Select(p => {
                            var status =
                                p.Delivered is not null ? PackageStatus.delivered
                                : p.PickedUp is not null ? PackageStatus.collected
                                : p.Scheduled is not null ? PackageStatus.assigned
                                : PackageStatus.created;
                            var dalSender = dal.GetCustomer(p.SenderId);
                            var sender = new PackageCustomer(dalSender.Id, dalSender.Name);
                            return new PackageInCustomer(p.Id, p.Weight, p.Priority, status, sender);
                        });
                    return new(id, dalCustomer.Name, dalCustomer.Phone, new(dalCustomer.Latitude, dalCustomer.Longitude),
                        sentPackages.ToList(), receivingPackages.ToList());
                    ;
                } catch {
                    throw new CustomerNotFoundException(id);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            lock (dal) {
                DO.Drone drone;
                try {
                    drone = dal.GetDrone(id);
                } catch (IdNotFoundException e) {
                    throw new DroneNotFoundException(id);
                }

                PackageInTransfer? pkg = null;
                var package = dal.GetPackageList(p => p.DroneId == id).Cast<DO.Package?>().FirstOrDefault();
                if (package is not null) {
                    var dalSender = dal.GetCustomer(package.Value.SenderId);
                    var dalRecipient = dal.GetCustomer(package.Value.TargetId);
                    var sender = new PackageCustomer(dalSender.Id, dalSender.Name);
                    var recipient = new PackageCustomer(dalRecipient.Id, dalRecipient.Name);
                    Location senderLoc = new(dalSender.Latitude, dalSender.Longitude);
                    Location recipLoc = new(dalRecipient.Latitude, dalRecipient.Longitude);
                    pkg = new(
                        package.Value.Id,
                        package.Value.Weight,
                        package.Value.Priority,
                        package.Value.PickedUp is not null,
                        sender,
                        recipient,
                        senderLoc,
                        recipLoc,
                        Utils.DistanceBetween(senderLoc, recipLoc));
                }

                var status = DroneStatus.free;
                if (package is not null) status = DroneStatus.delivering;
                else if (dal.GetDroneChargeList(dc => dc.DroneId == id).Any())
                    status = DroneStatus.maintenance;

                return new Drone(id, drone.Model, drone.MaxWeight, drone.Battery, status, pkg,
                    new(drone.Latitude, drone.Longitude));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Package GetPackage(int id)
        {
            lock (dal) {
                try {
                    var pkg = dal.GetPackage(id);

                    var dalSender = dal.GetCustomer(pkg.SenderId);
                    var sender = new PackageCustomer(dalSender.Id, dalSender.Name);
                    var dalRecip = dal.GetCustomer(pkg.TargetId);
                    var recipient = new PackageCustomer(dalRecip.Id, dalRecip.Name);

                    DroneInDelivery? droneInDelivery = null;
                    if (pkg.DroneId is not null) {
                        DroneListing drone = GetDroneList(d => d.Id == pkg.DroneId).First();
                        droneInDelivery = new(drone.Id, drone.BatteryStatus, drone.Location);
                    }

                    return new(id, sender, recipient, pkg.Weight, pkg.Priority, droneInDelivery, pkg.Requested,
                        pkg.Scheduled, pkg.PickedUp, pkg.Delivered);
                } catch {
                    throw new PackageNotFoundException(id);
                }
            }
        }
    }
}