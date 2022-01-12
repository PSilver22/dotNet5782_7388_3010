#nullable enable

using System;
using System.Collections.Generic;
using BL;

namespace BlApi
{
    public interface IBL
    {
        #region Add Methods
        /// <summary>
        /// Add a new base station
        /// </summary>
        /// <param name="id">id of the new base station</param>
        /// <param name="name">name of the new base station</param>
        /// <param name="latitude">latitude of the new base station</param>
        /// <param name="longitude">longitude of the new base station</param>
        /// <param name="numChargingSlots">number of charging slots in the new base station</param>
        public void AddBaseStation(int id, string name, double latitude, double longitude, int numChargingSlots);

        /// <summary>
        /// Add a new drone
        /// </summary>
        /// <param name="id">id of the new drone</param>
        /// <param name="model">model of the new drone</param>
        /// <param name="maxWeight">max weight of the new drone</param>
        /// <param name="startingStationId">the id of the station where the new drone will start</param>
        public void AddDrone(int id, string model, DO.WeightCategory maxWeight, int startingStationId);

        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="id">id of the new customer</param>
        /// <param name="name">name of the new customer</param>
        /// <param name="phone">phone of the new customer</param>
        /// <param name="longitude">longitude of the new customer</param>
        /// <param name="latitude">latitude of the new customer</param>
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);

        /// <summary>
        /// Add a new package
        /// </summary>
        /// <param name="senderId">id of the sender of the new package</param>
        /// <param name="receiverId">id of the receiver of the new package</param>
        /// <param name="weight">weight of the new package</param>
        /// <param name="priority">priority of the new package</param>
        /// <returns>ID of the new package</returns>
        public int AddPackage(int senderId, int receiverId, DO.WeightCategory weight, DO.Priority priority);

        #endregion

        #region Update Methods

        /// <summary>
        /// Update a base station
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">name</param>
        /// <param name="numChargingStations">number of charging stations</param>
        public void UpdateBaseStation(int id, string? name = null, int? numChargingStations = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void UpdateDrone(int id, string model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void UpdateCustomer(int id, string? name = null, string? phone = null);

        /// <summary>
        /// Send a drone to charge
        /// </summary>
        /// <param name="id">the id of the drone</param>
        public void SendDroneToCharge(int id);

        /// <summary>
        /// Release a drone from charging
        /// </summary>
        /// <param name="id">the id of the drone</param>
        public void ReleaseDroneFromCharge(int id);

        /// <summary>
        /// Assign a package to a drone
        /// </summary>
        /// <param name="id">the id of the drone</param>
        public void AssignPackageToDrone(int id);

        /// <summary>
        /// Have the drone collect its assigned package
        /// </summary>
        /// <param name="id">the id of the drone</param>
        public void CollectPackageByDrone(int id);

        /// <summary>
        /// Have the drone collect its package
        /// </summary>
        /// <param name="id">the id of the drone</param>
        public void DeliverPackageByDrone(int id);

        #endregion

        #region Get Methods

        /// <summary>
        /// Get a base station by id
        /// </summary>
        /// <param name="id">the id of the base station</param>
        /// <returns>the base station with the given id</returns>
        public BaseStation GetBaseStation(int id);

        /// <summary>
        /// Get a drone by id
        /// </summary>
        /// <param name="id">the id of the drone</param>
        /// <returns>the drone with the given id</returns>
        public Drone GetDrone(int id);

        /// <summary>
        /// Get a customer by id
        /// </summary>
        /// <param name="id">the id of the customer</param>
        /// <returns>the customer with the given id</returns>
        public Customer GetCustomer(int id);

        /// <summary>
        /// Get a package by id
        /// </summary>
        /// <param name="id">the id of the package</param>
        /// <returns>the package with the given id</returns>
        public Package GetPackage(int id);

        #endregion

        #region Get List Methods

        /// <summary>
        /// Get the list of base stations
        /// </summary>
        /// <param name = "filter" > The filter applied to the objects in the list</param>
        /// <returns>the list of base stations</returns>
        public IEnumerable<BaseStationListing> GetBaseStationList(Predicate<BaseStationListing>? filter = null);

        /// <summary>
        /// Get the list of drones
        /// </summary>
        /// <param name = "filter" > The filter applied to the objects in the list</param>
        /// <returns>the list of drones</returns>
        public IEnumerable<DroneListing> GetDroneList(Predicate<DroneListing>? filter = null);

        /// <summary>
        /// Get the list of customers
        /// </summary>
        /// <param name = "filter" > The filter applied to the objects in the list</param>
        /// <returns>the list of customers</returns>
        public IEnumerable<CustomerListing> GetCustomerList(Predicate<CustomerListing>? filter = null);

        /// <summary>
        /// Get the list of packages
        /// </summary>
        /// <param name = "filter" > The filter applied to the objects in the list</param>
        /// <returns>the list of packages</returns>
        public IEnumerable<PackageListing> GetPackageList(Predicate<PackageListing>? filter = null);

        #endregion
    }
}