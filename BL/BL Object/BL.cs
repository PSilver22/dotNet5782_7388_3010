#nullable enable

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
        private (double Free, double LightWeight, double MidWeight, double HeavyWeight, double ChargeRate) powerConsumption;

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
                        var closestStation = Utils.ClosestStation(senderLoc, stations);
                        location = new(closestStation.Latitude, closestStation.Longitude);
                    }
                    else
                    {
                        location = senderLoc;
                    }

                    // Calculate min required battery
                    var distToSender = package.PickedUp is null ? Utils.DistanceBetween(location, senderLoc) : 0;

                    var recipient = dal.GetCustomer(package.TargetId);
                    Location recipientLoc = new(recipient.Latitude, recipient.Longitude);

                    var distToRecip = Utils.DistanceBetween(location, recipientLoc);
                    var nearestStationToRecipient = Utils.ClosestStation(recipientLoc, stations);
                    var distToStation = Utils.DistanceBetween(recipientLoc, new(nearestStationToRecipient.Latitude, nearestStationToRecipient.Longitude));

                    var minBattery = (package.Weight switch
                    {
                        WeightCategory.light => powerConsumption.LightWeight,
                        WeightCategory.medium => powerConsumption.MidWeight,
                        WeightCategory.heavy => powerConsumption.HeavyWeight,
                        _ => 0
                    } * distToRecip) + (powerConsumption.Free * (distToSender + distToStation));

                    // Randomize battery
                    batteryStatus = minBattery + (rand.NextDouble() * (100.0 - minBattery));
                }
                else
                {
                    if ((rand.Next() & 1) == 0)
                    {
                        status = DroneStatus.free;
                        var deliveredPackages = packages.FindAll(p => p.Delivered is not null);
                        var customer = dal.GetCustomer(deliveredPackages[rand.Next(deliveredPackages.Count)].TargetId);
                        location = new(customer.Latitude, customer.Longitude);

                        var nearestStationToRecipient = Utils.ClosestStation(location, stations);
                        var distToStation = Utils.DistanceBetween(location, new(nearestStationToRecipient.Latitude, nearestStationToRecipient.Longitude));

                        // Randomize battery
                        var minBattery = powerConsumption.Free * distToStation;
                        batteryStatus = minBattery + (rand.NextDouble() * (100.0 - minBattery));
                    }
                    else
                    {
                        status = DroneStatus.maintenance;
                        var station = stations[rand.Next(stations.Count)];
                        location = new(station.Latitude, station.Longitude);

                        // Randomize battery
                        batteryStatus = rand.NextDouble() * 20;
                    }
                }

                return new DroneListing(d.Id, d.Model, d.MaxWeight, batteryStatus, status, location, packageId);
            });
        }
    }
}
