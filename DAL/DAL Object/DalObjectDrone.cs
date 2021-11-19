#nullable enable

using System;
using System.Linq;
using System.Collections.Generic;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// Adds a drone to the list of drones
        /// </summary>
        /// <param name="drone">The drone to add</param>
        /// <returns>true if the drone was successfully added, false otherwise</returns>
        public void AddDrone(Drone drone)
        {
            if (drone.Id < 0)
            {
                throw new InvalidIdException(drone.Id);
            }

            if (DataSource.drones.Count >= DataSource.MaxDrones)
            {
                throw new MaximumCapacityException("Drone list is at maximum capacity.");
            }

            DataSource.drones.Add(drone);

        }

        /// <summary>
        /// Assigns a package to a drone
        /// </summary>
        /// <param name="packageId">The ID of the package</param>
        /// <returns>true if the package was successfully assigned to a drone, false otherwise</returns>
        public void AssignPackage(int packageId)
        {
            if (packageId < 0)
            {
                throw new InvalidIdException(packageId);
            }

            var package = GetPackage(packageId);

            // Find a free drone with the minimum required weight, in order to
            // efficiently assign more capable drones.
            int droneIndex = -1;
            for (var i = 0; i < DataSource.drones.Count; i++)
            {
                var d = DataSource.drones[i];
                
                if (d.MaxWeight == package.Weight)
                {
                    droneIndex = i;
                    break;
                }

                if (d.MaxWeight > package.Weight)
                {
                    // If this is the first available drone, or a better choice
                    // use it.
                    if (droneIndex == -1 || DataSource.drones[droneIndex].MaxWeight > d.MaxWeight)
                    {
                        droneIndex = i;
                    }
                }
            }

            if (droneIndex == -1) throw new IdNotFoundException(droneIndex);

            Drone drone = DataSource.drones[droneIndex];

            package.DroneId = drone.Id;
            package.Scheduled = DateTime.UtcNow;

            DataSource.drones[droneIndex] = drone;
            DataSource.packages[GetPackageIndex(packageId)] = package;
        }

        /// <summary>
        /// Collects a package that's been assigned to a drone
        /// </summary>
        /// <param name="packageId">The ID of the package to collect</param>
        public void CollectPackage(int packageId)
        {
            if (packageId < 0)
            {
                throw new InvalidIdException(packageId);
            }

            var package = GetPackage(packageId);
            if (package.Scheduled is not null)
                package.PickedUp = DateTime.UtcNow;

            DataSource.packages[GetPackageIndex(packageId)] = package;
        }

        /// <summary>
        /// Provide a package that's been collected by a drone to the customer
        /// </summary>
        /// <param name="packageId">The ID of the package to provide</param>
        public void ProvidePackage(int packageId)
        {
            if (packageId < 0)
            {
                throw new InvalidIdException(packageId);
            }

            var package = GetPackage(packageId);
            if (package.PickedUp is not null)
            {
                // Set the delivery time and mark the drone as free
                package.Delivered = DateTime.UtcNow;
                var drone = GetDrone(package.DroneId ?? -1);

                DataSource.drones[GetDroneIndex(package.DroneId ?? -1)] = drone;
            }

            DataSource.packages[GetPackageIndex(packageId)] = package;
        }

        /// <summary>
        /// Sends a drone to a charging station
        /// </summary>
        /// <param name="droneId">The ID of the drone</param>
        /// <param name="stationId">The ID of the station</param>
        public void ChargeDrone(int droneId, int stationId)
        {
            if (droneId < 0 || stationId < 0)
            {
                throw new InvalidIdException((droneId < 0) ? droneId : stationId);
            }

            var station = GetStation(stationId);

            if (station.ChargeSlots > 0)
            {
                var droneCharge = new DroneCharge(droneId, stationId);

                DataSource.droneCharges.Add(droneCharge);

                station.ChargeSlots--;
            }

            DataSource.stations[GetStationIndex(stationId)] = station;
        }

        /// <summary>
        /// Releases a drone from a charging station
        /// </summary>
        /// <param name="droneId">The ID of the drone</param>
        public void ReleaseDrone(int droneId)
        {
            if (droneId < 0)
            {
                throw new InvalidIdException(droneId);
            }

            var droneChargeIndex = DataSource.droneCharges.FindIndex(dc => dc.DroneId == droneId);
            var station = GetStation(DataSource.droneCharges[droneChargeIndex].StationId);
            station.ChargeSlots++;
            DataSource.droneCharges.RemoveAt(droneChargeIndex);

            DataSource.stations[GetStationIndex(DataSource.droneCharges[droneChargeIndex].StationId)] = station;
        }

        /// <summary>
        /// gets a drone by id from the drones array
        /// </summary>
        /// <param name="id">id of the drone</param>
        /// <returns>Drone with the given id if found. throws otherwise</returns>
        public Drone GetDrone(int id)
        {
            return GetItemByKey<Drone>(id, DataSource.drones);
        }

        /// <summary>
        /// Creates a string with the information for every drone in the drones list
        /// </summary>
        /// <returns>
        /// Drone list
        /// </returns>
        public List<Drone> GetDroneList()
        {
            return DataSource.drones;
        }

        /// <summary>
        /// Returns the list of drone charges
        /// </summary>
        /// <returns>Drone charge list</returns>
        public List<DroneCharge> GetDroneChargeList()
        {
            return DataSource.droneCharges;
        }

        /// <summary>
        /// Returns the charge of the given drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public DroneCharge GetDroneCharge(int droneId)
        {
            return DataSource.droneCharges.Find(x => x.DroneId == droneId);
        }

        /// <summary>
        /// Removes charge from the drone charge list with the given drone id
        /// </summary>
        /// <param name="droneId"></param>
        public void DeleteDroneCharge(int droneId)
        {
            DataSource.droneCharges.Remove(GetDroneCharge(droneId));
        }

        /// <summary>
        /// Adds new drone charge to the database
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="droneId"></param>
        public void AddDroneCharge(int stationId, int droneId)
        {
            DataSource.droneCharges.Add(new DroneCharge(stationId, droneId));
        }

        /// <summary>
        /// gets a drone's index by id from the drones array
        /// </summary>
        /// <param name="id">id of the drone</param>
        /// <returns>Index of the drone with the given id if found.</returns>
        private int GetDroneIndex(int id)
        {
            return GetItemIndexByKey<Drone>(id, DataSource.drones);
        }

        /// <summary>
        /// Updates the drone with the given id's properties to the given values
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="battery"></param>
        public void UpdateDrone(int id, string? model = null, WeightCategory? maxWeight = null, double? battery = null)
        {
            int index = GetDroneIndex(id);

            Drone updatedDrone = DataSource.drones[id];

            updatedDrone.Model = model ?? updatedDrone.Model;
            updatedDrone.MaxWeight = maxWeight ?? updatedDrone.MaxWeight;
            updatedDrone.Battery = battery ?? updatedDrone.Battery;

            SetDrone(updatedDrone);
        }

        /// <summary>
        /// Sets the drone with a matching id in the database to the given drone.
        /// </summary>
        /// <param name="drone"></param>
        public void SetDrone(Drone drone)
        {
            int index = GetDroneIndex(drone.Id);

            DataSource.drones[index] = drone;
        }

        /// <summary>
        /// Getter for the power consumption rates
        /// </summary>
        /// <returns> A double with the power consumption rates in the order: free, light-weight, mid-weight, heavy-weight, charging-rate</returns>
        public (double, double, double, double, double) GetPowerConsumption()
        {
            return (
                DataSource.Config.free,
                DataSource.Config.lightWeight,
                DataSource.Config.midWeight,
                DataSource.Config.heavyWeight,
                DataSource.Config.chargeRate);
        }
    }
}
