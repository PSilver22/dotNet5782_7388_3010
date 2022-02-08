#nullable enable

using System;
using System.Linq;
using System.Collections.Generic;
using BL;
using DO;

namespace BlApi
{
    public partial class BL : IBL
    {
        private List<DroneListing> drones;
        private DalApi.IDAL dal;
        private (double Free, double LightWeight, double MidWeight, double HeavyWeight, double ChargeRate) powerConsumption;
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
            // Convert to list so we can get a random station without
            // double-enumeration (count + access).
            var stations = dal.GetStationList().ToList();

            drones = dal.GetDroneList().Select(d =>
            {
                Location location;
                double batteryStatus;
                DroneStatus status;
                int? packageId = null;
                
                status = DroneStatus.maintenance;
                var station = stations[rand.Next(stations.Count)];
                location = new Location(station.Latitude, station.Longitude);

                // Randomize battery
                batteryStatus = rand.NextDouble() * 20;

                dal.UpdateDrone(d.Id, battery: batteryStatus);
                dal.UpdateStation(station.Id, chargeSlots: station.ChargeSlots - 1);
                dal.AddDroneCharge(station.Id, d.Id);

                return new DroneListing(d.Id, d.Model, d.MaxWeight, batteryStatus, status, location, packageId);
            }).ToList();
        }
    }
}
