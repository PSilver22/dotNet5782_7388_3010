#nullable enable

using System;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        // TODO: Add documentation

        // Add
        public void AddBaseStation(int id, string name, double latitude, double longitude, int numChargingSlots);
        public void AddDrone(int id, string model, IDAL.DO.WeightCategory maxWeight, int startingStationId);
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        public void AddPackage(int senderId, int receiverId, IDAL.DO.WeightCategory weight, IDAL.DO.Priority priority);

        // Update
        public void UpdateBaseStation(int id, string? name = null, int? numChargingStations = null);
        public void UpdateDrone(int id, string model);
        public void UpdateCustomer(int id, string? name = null, string? phone = null);

        public void SendDroneToCharge(int id);
        public void ReleaseDroneFromCharge(int id, int chargingTime);

        public void AssignPackageToDrone(int id);
        public void CollectPackageByDrone(int id);
        public void DeliverPackageByDrone(int id);

        // Get
        public BO.BaseStation GetBaseStation(int id);
        public BO.Drone GetDrone(int id);
        public BO.Customer GetCustomer(int id);
        public BO.Package GetPackage(int id);

        // Get List
        public IEnumerable<BO.BaseStationListing> GetBaseStationList();
        public IEnumerable<BO.DroneListing> GetDroneList();
        public IEnumerable<BO.CustomerListing> GetCustomerList();
        public IEnumerable<BO.PackageListing> GetPackageList();
        public IEnumerable<BO.PackageListing> GetUnassignedPackageList();
        public IEnumerable<BO.BaseStationListing> GetAvailableChargingStations();
    }
}
