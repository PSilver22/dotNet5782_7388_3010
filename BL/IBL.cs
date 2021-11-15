#nullable enable

using System;
namespace BL
{
    public interface IBL
    {
        // Add
        void AddBaseStation(int id, string name, double latitude, double longitude);
        void AddDrone(int id, string model, IDAL.DO.WeightCategory maxWeight, int startingStationId);
        void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        void AddPackage(int senderId, int receiverId, IDAL.DO.WeightCategory weight, IDAL.DO.Priority priority);

        // Update
        void UpdateBaseStaion(int id, string? name = null, int? numChargingStations = null);
        void UpdateDrone(int id, string model);
        void UpdateCustomer(int id, string? name = null, string? phone = null);

        void SendDroneToCharge(int id);
        void ReleaseDroneFromCharge(int id, int chargingTime);

        void AssignPackageToDrone(int id);
        void CollectPackageByDrone(int id);
        void DeliverPackageByDrone(int id);

        // Display
        void DisplayBaseStation(int id);
        void DisplayDrone(int id);
        void DisplayCustomer(int id);
        void DisplayPackage(int id);

        // List Display
        void DisplayBaseStationList();
        void DisplayDroneList();
        void DisplayCustomerList();
        void DisplayPackageList();
        void DisplayUnassignedPackageList();
        void DisplayAvailableChargingStations();
    }
}
