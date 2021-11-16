#nullable enable

using System;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        // Add
        void AddBaseStation(int id, string name, double latitude, double longitude, int numChargingSlots);
        void AddDrone(int id, string model, IDAL.DO.WeightCategory maxWeight, int startingStationId);
        void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        void AddPackage(int senderId, int receiverId, IDAL.DO.WeightCategory weight, IDAL.DO.Priority priority);

        // Update
        void UpdateBaseStation(int id, string? name = null, int? numChargingStations = null);
        void UpdateDrone(int id, string model);
        void UpdateCustomer(int id, string? name = null, string? phone = null);

        void SendDroneToCharge(int id);
        void ReleaseDroneFromCharge(int id, int chargingTime);

        void AssignPackageToDrone(int id);
        void CollectPackageByDrone(int id);
        void DeliverPackageByDrone(int id);

        // Get
        BO.BaseStation GetBaseStation(int id);
        BO.Drone GetDrone(int id);
        BO.Customer GetCustomer(int id);
        BO.Package GetPackage(int id);

        // Get List
        IEnumerable<BO.BaseStationListing> GetBaseStationList();
        IEnumerable<BO.DroneListing> GetDroneList();
        IEnumerable<BO.CustomerListing> GetCustomerList();
        IEnumerable<BO.PackageListing> GetPackageList();
        IEnumerable<BO.PackageListing> GetUnassignedPackageList();
        IEnumerable<BO.BaseStationListing> GetAvailableChargingStations();
    }
}
