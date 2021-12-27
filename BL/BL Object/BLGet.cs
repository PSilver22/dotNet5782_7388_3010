#nullable enable

using System;
using BL;

namespace BlApi
{
    public partial class BL : IBL
    {
        public BaseStation GetBaseStation(int id)
        {
            try
            {
                var dalStation = dal.GetStation(id);
                var chargingDrones = dal.GetDroneChargeList()
                    .FindAll(cd => cd.StationId == id)
                    .ConvertAll(cd => new ChargingDrone(cd.DroneId, dal.GetDrone(cd.DroneId).Battery));
                return new(id, dalStation.Name, new(dalStation.Latitude, dalStation.Longitude), dalStation.ChargeSlots, chargingDrones);
            }
            catch (IDAL.DO.IdNotFoundException)
            {
                throw new StationNotFoundException(id);
            }
        }

        public Customer GetCustomer(int id)
        {
            try
            {
                var dalCustomer = dal.GetCustomer(id);
                var packages = dal.GetPackageList();
                var sentPackages = packages
                    .FindAll(p => p.SenderId == id)
                    .ConvertAll(p =>
                    {
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
                    .FindAll(p => p.TargetId == id)
                    .ConvertAll(p =>
                     {
                         var status =
                               p.Delivered is not null ? PackageStatus.delivered
                             : p.PickedUp is not null ? PackageStatus.collected
                             : p.Scheduled is not null ? PackageStatus.assigned
                             : PackageStatus.created;
                         var dalSender = dal.GetCustomer(p.SenderId);
                         var sender = new PackageCustomer(dalSender.Id, dalSender.Name);
                         return new PackageInCustomer(p.Id, p.Weight, p.Priority, status, sender);
                     });
                return new(id, dalCustomer.Name, dalCustomer.Phone, new(dalCustomer.Latitude, dalCustomer.Longitude), sentPackages, receivingPackages); ;
            }
            catch
            {
                throw new CustomerNotFoundException(id);
            }
        }

        public BL.Drone GetDrone(int id)
        {
            var droneIndex = drones.FindIndex(d => d.Id == id);
            if (droneIndex == -1)
            { throw new DroneNotFoundException(id); }

            var drone = drones[droneIndex];

            PackageInTransfer? pkg = null;
            if (drone.PackageId is not null)
            {
                var dalPackage = dal.GetPackageList().Find(p => p.DroneId == id);
                var dalSender = dal.GetCustomer(dalPackage.SenderId);
                var dalRecipient = dal.GetCustomer(dalPackage.TargetId);
                var sender = new PackageCustomer(dalSender.Id, dalSender.Name);
                var recipient = new PackageCustomer(dalRecipient.Id, dalRecipient.Name);
                Location senderLoc = new(dalSender.Latitude, dalSender.Longitude);
                Location recipLoc = new(dalRecipient.Latitude, dalRecipient.Longitude);
                pkg = new(
                    dalPackage.Id,
                    dalPackage.Weight,
                    dalPackage.Priority,
                    dalPackage.PickedUp is not null,
                    sender,
                    recipient,
                    senderLoc,
                    recipLoc,
                    Utils.DistanceBetween(senderLoc, recipLoc));
            }
            return new Drone(id, drone.Model, drone.WeightCategory, drone.BatteryStatus, drone.Status, pkg, drone.Location);
        }

        public Package GetPackage(int id)
        {
            try
            {
                var pkg = dal.GetPackage(id);

                var dalSender = dal.GetCustomer(pkg.SenderId);
                var sender = new PackageCustomer(dalSender.Id, dalSender.Name);
                var dalRecip = dal.GetCustomer(pkg.TargetId);
                var recipient = new PackageCustomer(dalRecip.Id, dalRecip.Name);

                DroneInDelivery? droneInDelivery = null;
                if (pkg.DroneId is not null)
                {
                    DroneListing drone = drones.Find(d => d.Id == pkg.DroneId)!;
                    droneInDelivery = new(drone.Id, drone.BatteryStatus, drone.Location);
                }

                return new(id, sender, recipient, pkg.Weight, pkg.Priority, droneInDelivery, pkg.Requested, pkg.Scheduled, pkg.PickedUp, pkg.Delivered);
            }
            catch
            {
                throw new PackageNotFoundException(id);
            }
        }
    }
}
