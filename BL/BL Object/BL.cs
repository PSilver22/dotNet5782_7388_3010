#nullable enable

using System;
using System.Collections.Generic;
using BL;

namespace BlApi
{
    public partial class BL : IBL
    {
        private List<DroneListing> drones;
        internal DalApi.IDAL dal;
        internal (double Free, double LightWeight, double MidWeight, double HeavyWeight, double ChargeRate) powerConsumption;
        private static readonly Lazy<BL> instance = new(() => new BL());

        private readonly Random rand = new();

        internal static BL Instance {
            get 
            {
                return instance.Value;
            }
        }

        private BL()
        {
            dal = new DalObject.DalObject();

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

                status = DroneStatus.maintenance;
                var station = stations[rand.Next(stations.Count)];
                location = new(station.Latitude, station.Longitude);

                // Randomize battery
                batteryStatus = rand.NextDouble() * 20;

                dal.UpdateDrone(d.Id, battery: batteryStatus);
                dal.UpdateStation(station.Id, chargeSlots: station.ChargeSlots - 1);
                dal.AddDroneCharge(station.Id, d.Id);

                return new DroneListing(d.Id, d.Model, d.MaxWeight, batteryStatus, status, location, packageId);
            });
        }
    }
}
