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

            if (DataSource.drones.Count < DataSource.MaxDrones)
            {
                DataSource.drones.Add(drone);
            }

            throw new MaximumCapacityException("Drone list is at maximum capacity.");
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
                if (d.Status == DroneStatus.free)
                {
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
            }

            if (droneIndex == -1) throw new IdNotFoundException(droneIndex);

            Drone drone = DataSource.drones[droneIndex];

            package.DroneId = drone.Id;
            package.Scheduled = DateTime.UtcNow;

            drone.Status = DroneStatus.delivery;

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
                var drone = GetDrone(package.DroneId);
                drone.Status = DroneStatus.free;

                DataSource.drones[GetDroneIndex(package.DroneId)] = drone;
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
        /// string with the information for every drone
        /// </returns>
        public string GetDroneList()
        {
            return ListItems<Drone>(DataSource.drones);
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
        /// Getter for the power consumption rates
        /// </summary>
        /// <returns> A double with the power consumption rates in the order: free, light-weight, mid-weight, heavy-weight, charging-rate</returns>
        public double[] GetPowerConsumption()
        {
            double[] result = {
                DataSource.Config.free,
                DataSource.Config.lightWeight,
                DataSource.Config.midWeight,
                DataSource.Config.heavyWeight,
                DataSource.Config.chargeRate };

            return result;
        }
    }
}
