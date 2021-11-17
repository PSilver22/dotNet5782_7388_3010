#nullable enable

using System;
using System.Collections.Generic;
using IDAL.DO;

namespace IDAL
{
    public interface IDAL
    {
        public void AddStation(Station station);

        public void AddDrone(Drone drone);

        public void AddCustomer(Customer customer);

        public void AddPackage(int senderId, int targetId, WeightCategory weight, Priority priority);

        public void AddDroneCharge(int stationId, int droneId);

        public Station GetStation(int id);

        public Drone GetDrone(int id);

        public Customer GetCustomer(int id);

        public Package GetPackage(int id);

        public void SetStation(Station station);

        public void SetDrone(Drone drone);

        public void SetCustomer(Customer customer);

        public void SetPackage(Package package);

        public void UpdateStation(int id, string? name, double? longitude, double? latitude, int? chargeSlots);

        public void UpdateDroneDrone(int id, string? model, WeightCategory? maxWeight, double? battery);

        public void UpdateCustomer(int id, string? name = null, string? phone = null, double? longitude = null, double? latitude = null);

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
        /// <param name="scheduled">use DateTime(0) to set to null</param>
        /// <param name="pickedUp">use DateTime(0) to set to null</param>
        /// <param name="delivered">use DateTime(0) to set to null</param>
        public void UpdatePackage(
                int id,
                int? senderId,
                int? targetId,
                WeightCategory? weight,
                Priority? priority,
                DateTime? requested,
                int? droneId,
                DateTime? scheduled,
                DateTime? pickedUp,
                DateTime? delivered);

        public void DeleteDroneCharge(int droneId);

        public List<Station> GetStationList();

        public List<Station> GetUnoccupiedStationsList();

        public List<Drone> GetDroneList();

        public List<Package> GetPackageList();

        public List<Package> GetUnassignedPackageList();

        public List<Customer> GetCustomerList();

        public (double Free, double LightWeight, double MidWeight, double HeavyWeight, double ChargeRate) GetPowerConsumption();
    }
}
