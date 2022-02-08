#nullable enable

using System;
using System.Collections.Generic;
using DO;

namespace DalApi
{
    public interface IDAL
    {
        #region Add Methods

        public void AddStation(Station station);

        public void AddDrone(Drone drone);

        public void AddCustomer(Customer customer);

        public int AddPackage(int senderId, int targetId, WeightCategory weight, Priority priority);

        public void AddDroneCharge(int stationId, int droneId);

        #endregion

        #region Get Methods

        public Station GetStation(int id);

        public Drone GetDrone(int id);

        public Customer GetCustomer(int id);

        public Package GetPackage(int id);

        public DroneCharge GetDroneCharge(int droneId);

        #endregion

        #region Set Methods

        public void SetStation(Station station);

        public void SetDrone(Drone drone);

        public void SetCustomer(Customer customer);

        public void SetPackage(Package package);

        #endregion

        #region Update Methods

        public void UpdateStation(int id, string? name = null, double? longitude = null, double? latitude = null,
            int? chargeSlots = null);

        public void UpdateDrone(int id, string? model = null, WeightCategory? maxWeight = null, double? battery = null, double? longitude = null, double? latitude = null);

        public void UpdateCustomer(int id, string? name = null, string? phone = null, double? longitude = null,
            double? latitude = null);

        /// <summary>
        /// Update the supplied fields in the package with the given id.
        /// Throws if no station with the given id is found.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        /// <param name="requested"></param>
        /// <param name="droneId"></param>
        /// <param name="scheduled"></param>
        /// <param name="pickedUp"></param>
        /// <param name="delivered"></param>
        public void UpdatePackage(
            int id,
            int? senderId = null,
            int? targetId = null,
            WeightCategory? weight = null,
            Priority? priority = null,
            DateTime? requested = null,
            int? droneId = null,
            DateTime? scheduled = null,
            DateTime? pickedUp = null,
            DateTime? delivered = null);

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
            if (package.Scheduled is not null && package.PickedUp is null)
                UpdatePackage(packageId, pickedUp: DateTime.UtcNow);
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
            if (package.PickedUp is null || package.Delivered is not null) return;
            package.Delivered = DateTime.UtcNow;
            UpdatePackage(packageId, delivered: DateTime.UtcNow);
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
                AddDroneCharge(stationId, droneId);
                UpdateStation(stationId, chargeSlots: station.ChargeSlots - 1);
            }
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

            var stationId = GetDroneCharge(droneId).StationId;
            DeleteDroneCharge(droneId);
            var station = GetStation(stationId);
            UpdateStation(stationId, chargeSlots: station.ChargeSlots + 1);
        }

        public void DeleteDroneCharge(int droneId);

        #endregion

        #region Get List Methods

        public IEnumerable<Station> GetStationList(Predicate<Station>? filter = null);

        //public List<Station> GetUnoccupiedStationsList();

        public IEnumerable<Drone> GetDroneList(Predicate<Drone>? filter = null);

        public IEnumerable<Package> GetPackageList(Predicate<Package>? filter = null);

        public IEnumerable<Customer> GetCustomerList(Predicate<Customer>? filter = null);

        public IEnumerable<DroneCharge> GetDroneChargeList(Predicate<DroneCharge>? filter = null);

        #endregion

        public (double Free, double LightWeight, double MidWeight, double HeavyWeight, double ChargeRate)
            GetPowerConsumption();
    }
}