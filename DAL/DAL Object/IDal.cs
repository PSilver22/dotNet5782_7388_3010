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

        public int AddPackage(int senderId, int targetId, WeightCategory weight, Priority priority);

        public void AddDroneCharge(int stationId, int droneId);

        public Station GetStation(int id);

        public Drone GetDrone(int id);

        public Customer GetCustomer(int id);

        public Package GetPackage(int id);

        public DroneCharge GetDroneCharge(int droneId);

        public void SetStation(Station station);

        public void SetDrone(Drone drone);

        public void SetCustomer(Customer customer);

        public void SetPackage(Package package);

        public void UpdateStation(int id, string? name = null, double? longitude = null, double? latitude = null, int? chargeSlots = null);

        public void UpdateDrone(int id, string? model = null, WeightCategory? maxWeight = null, double? battery = null);

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
                int? senderId = null,
                int? targetId = null,
                WeightCategory? weight = null,
                Priority? priority = null,
                DateTime? requested = null,
                int? droneId = null,
                DateTime? scheduled = null,
                DateTime? pickedUp = null,
                DateTime? delivered = null);

        public void DeleteDroneCharge(int droneId);

        public List<Station> GetStationList(Predicate<Station>? filter = null);

        //public List<Station> GetUnoccupiedStationsList();

        public List<Drone> GetDroneList(Predicate<Drone>? filter = null);

        public List<Package> GetPackageList(Predicate<Package>? filter = null);

        public List<Customer> GetCustomerList(Predicate<Customer>? filter = null);

        public List<DroneCharge> GetDroneChargeList(Predicate<DroneCharge>? filter = null);

        public (double Free, double LightWeight, double MidWeight, double HeavyWeight, double ChargeRate) GetPowerConsumption();
    }
}
