using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;
using IDAL.DO;

namespace IBL
{
    public partial class BL : IBL
    {
        private List<DroneListing> drones;
        private IDAL.IDAL dal;
        private double[] powerConsumption;

        public BL()
        {
            dal = new DalObject.DalObject();

            var rand = new Random();

            powerConsumption = dal.GetPowerConsumption();

            var packages = dal.GetPackageList();
            var stations = dal.GetStationList();

            drones = dal.GetDroneList().ConvertAll(d =>
            {
                Location location;
                double batteryStatus;
                DroneStatus status;
                int? packageId = null;

                // TODO: random attery status
                batteryStatus = 100;

                var packageIndex = packages.FindIndex(p => p.Delivered is null && p.DroneId == d.Id);

                if (packageIndex != -1)
                {
                    var package = packages[packageIndex];
                    packageId = package.Id;

                    status = DroneStatus.delivering;

                    var sender = dal.GetCustomer(package.SenderId);
                    var senderLoc = location = new(sender.Latitude, sender.Longitude);

                    if (package.PickedUp is null)
                    {
                        (location, _) = stations.ConvertAll<(Location loc, double dist)>(s =>
                        {
                            Location sLoc = new(s.Latitude, s.Longitude);
                            return (sLoc, Utils.DistanceBetween(senderLoc, sLoc));
                        }).Aggregate((curr, next) => curr.dist < next.dist ? curr : next);
                    }
                    else
                    {
                        location = senderLoc;
                    }
                }
                else
                {
                    if ((rand.Next() & 1) == 0)
                    {
                        status = DroneStatus.free;
                        var deliveredPackages = packages.FindAll(p => p.Delivered is not null);
                        var customer = dal.GetCustomer(deliveredPackages[rand.Next(deliveredPackages.Count)].TargetId);
                        location = new(customer.Latitude, customer.Longitude);
                    }
                    else
                    {
                        status = DroneStatus.maintenance;
                        var station = stations[rand.Next(stations.Count)];
                        location = new(station.Latitude, station.Longitude);
                    }
                }

                return new DroneListing(d.Id, d.Model, d.MaxWeight, batteryStatus, status, location, packageId);
            });
        }
    }
}
