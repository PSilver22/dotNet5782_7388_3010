#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BL;
using BlApi;
using DO;
using Customer = BL.Customer;
using Drone = BL.Drone;
using Package = BL.Package;

namespace PL
{
    /// <summary>
    /// Wraps an instance of IBL, maintaining ObservableCollection's of each
    /// listing, and wrapping update operations to keep the collections
    /// up-to-date.
    /// </summary>
    public class DataManager: IBL
    {
        private readonly IBL _bl;
        
        public ObservableCollection<BaseStationListing> Stations { get; }
        public ObservableCollection<CustomerListing> Customers { get; }
        public ObservableCollection<DroneListing> Drones { get; }
        public ObservableCollection<PackageListing> Packages { get; }

        public DataManager(IBL bl)
        {
            _bl = bl;
            Stations = new (_bl.GetBaseStationList());
            Customers = new (_bl.GetCustomerList());
            Drones = new (_bl.GetDroneList());
            Packages = new (_bl.GetPackageList());
        }

        private BaseStationListing RefreshStationListing(int id)
        {
            Stations.Remove(Stations.Single(s => s.Id == id));
            var station = _bl.GetBaseStationList().First(s => s.Id == id);
            Stations.Add(station);
            return station;
        }

        private CustomerListing RefreshCustomerListing(int id)
        {
            Customers.Remove(Customers.Single(c => c.Id == id));
            var customer = _bl.GetCustomerList().First(c => c.Id == id);
            Customers.Add(customer);
            return customer;
        }

        private DroneListing RefreshDroneListing(int id)
        {
            Drones.Remove(Drones.Single(d => d.Id == id));
            var drone = _bl.GetDroneList().First(d => d.Id == id);
            Drones.Add(drone);
            return drone;
        }

        private PackageListing RefreshPackageListing(int id)
        {
            Packages.Remove(Packages.Single(p => p.Id == id));
            var package = _bl.GetPackageList().First(p => p.Id == id);
            Packages.Add(package);
            return package;
        }
        
        public void AddBaseStation(int id, string name, double latitude, double longitude, int numChargingSlots)
        {
            _bl.AddBaseStation(id, name, latitude, longitude, numChargingSlots);
            Stations.Add(_bl.GetBaseStationList().First(s => s.Id == id));
        }

        public void AddDrone(int id, string model, WeightCategory maxWeight, int startingStationId)
        {
            _bl.AddDrone(id, model, maxWeight, startingStationId);
            Drones.Add(_bl.GetDroneList().First(d => d.Id == id));
        }

        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            _bl.AddCustomer(id, name, phone, longitude, latitude);
            Customers.Add(_bl.GetCustomerList().First(c => c.Id == id));
        }

        public int AddPackage(int senderId, int receiverId, WeightCategory weight, Priority priority)
        {
            var id = _bl.AddPackage(senderId, receiverId, weight, priority);
            Packages.Add(_bl.GetPackageList().First(p => p.Id == id));
            RefreshCustomerListing(senderId);
            RefreshCustomerListing(receiverId);
            return id;
        }

        public void UpdateBaseStation(int id, string? name = null, int? numChargingStations = null)
        {
            _bl.UpdateBaseStation(id, name, numChargingStations);
            RefreshStationListing(id);
        }

        public void UpdateDrone(int id, string model)
        {
            _bl.UpdateDrone(id, model);
            RefreshDroneListing(id);
        }

        public void UpdateCustomer(int id, string? name = null, string? phone = null)
        {
            _bl.UpdateCustomer(id, name, phone);
            RefreshCustomerListing(id);
            var customer = _bl.GetCustomer(id);
            foreach (var package in customer.ReceivingPackages)
                RefreshPackageListing(package.Id);
            foreach (var package in customer.SentPackages)
                RefreshPackageListing(package.Id);
        }

        public void SendDroneToCharge(int id)
        {
            _bl.SendDroneToCharge(id);
            var drone = RefreshDroneListing(id);
            if (drone.ChargingStationId.HasValue)
                RefreshStationListing(drone.ChargingStationId.Value);
        }

        public void ReleaseDroneFromCharge(int id)
        {
            var drone = Drones.FirstOrDefault(d => d.Id == id);
            _bl.ReleaseDroneFromCharge(id);
            RefreshDroneListing(id);
            if (drone?.ChargingStationId.HasValue ?? false)
                RefreshStationListing(drone.ChargingStationId.Value);
        }

        public void AssignPackageToDrone(int id)
        {
            _bl.AssignPackageToDrone(id);
            var drone = RefreshDroneListing(id);
            RefreshPackageListing(drone.PackageId!.Value);
        }

        public void CollectPackageByDrone(int id)
        {
            _bl.CollectPackageByDrone(id);
            var drone = RefreshDroneListing(id);
            RefreshPackageListing(drone.PackageId!.Value);
        }

        public void DeliverPackageByDrone(int id)
        {
            var drone = Drones.FirstOrDefault(d => d.Id == id);
            _bl.DeliverPackageByDrone(id);
            RefreshDroneListing(id);
            RefreshPackageListing(drone!.PackageId!.Value);
        }

        public BaseStation GetBaseStation(int id)
        {
            return _bl.GetBaseStation(id);
        }

        public Drone GetDrone(int id)
        {
            return _bl.GetDrone(id);
        }

        public Customer GetCustomer(int id)
        {
            return _bl.GetCustomer(id);
        }

        public Package GetPackage(int id)
        {
            return _bl.GetPackage(id);
        }

        public IEnumerable<BaseStationListing> GetBaseStationList(Predicate<BaseStationListing>? filter = null)
        {
            return _bl.GetBaseStationList(filter);
        }

        public IEnumerable<DroneListing> GetDroneList(Predicate<DroneListing>? filter = null)
        {
            return _bl.GetDroneList(filter);
        }

        public IEnumerable<CustomerListing> GetCustomerList(Predicate<CustomerListing>? filter = null)
        {
            return _bl.GetCustomerList(filter);
        }

        public IEnumerable<PackageListing> GetPackageList(Predicate<PackageListing>? filter = null)
        {
            return _bl.GetPackageList(filter);
        }

        public void StartSimulator(int id, Func<bool> condition, Action update)
        {
            _bl.StartSimulator(id, condition, update);
        }
    }
}