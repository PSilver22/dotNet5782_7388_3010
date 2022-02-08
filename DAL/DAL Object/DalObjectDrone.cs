#nullable enable

using System;
using System.Linq;
using System.Collections.Generic;
using DO;
using DalApi;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// Adds a drone to the list of drones
        /// </summary>
        /// <param name="drone">The drone to add</param>
        public void AddDrone(Drone drone)
        {
            if (drone.Id < 0)
            {
                throw new InvalidIdException(drone.Id);
            }

            if (DataSource.drones.Exists(d => d.Id == drone.Id))
            {
                throw new DuplicatedIdException(drone.Id, "drone");
            }

            DataSource.drones.Add(drone);

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
        /// <param name="filter">The filter applied to the objects in the list</param>
        /// <returns>
        /// Drone list
        /// </returns>
        public IEnumerable<Drone> GetDroneList(Predicate<Drone>? filter = null)
        {
            return DataSource.drones.Where(new Func<Drone, bool>(filter ?? (x=> true))).ToList();
        }

        /// <summary>
        /// Returns the list of drone charges
        /// </summary>
        /// <param name="filter">The filter applied to the objects in the list</param>
        /// <returns>Drone charge list</returns>
        public IEnumerable<DroneCharge> GetDroneChargeList(Predicate<DroneCharge>? filter = null)
        {
            return DataSource.droneCharges.Where(new Func<DroneCharge, bool>(filter ?? (x => true))).ToList();
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
            DataSource.droneCharges.Add(new DroneCharge(stationId, droneId, DateTime.UtcNow));
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
        public void UpdateDrone(int id, string? model = null, WeightCategory? maxWeight = null, double? battery = null, double? longitude = null, double? latitude = null)
        {
            int index = GetDroneIndex(id);

            Drone updatedDrone = DataSource.drones[index];

            updatedDrone.Model = model ?? updatedDrone.Model;
            updatedDrone.MaxWeight = maxWeight ?? updatedDrone.MaxWeight;
            updatedDrone.Battery = battery ?? updatedDrone.Battery;
            updatedDrone.Longitude = longitude ?? updatedDrone.Longitude;
            updatedDrone.Latitude = latitude ?? updatedDrone.Latitude;

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
