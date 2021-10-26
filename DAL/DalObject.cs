using System;
using System.Linq;
using IDAL.DO;

namespace DalObject
{
    /// <summary>
    /// A class that interacts with and performs operations
    /// on the data source class
    /// </summary>
    public class DalObject
    {
        /// <summary>
        /// Initializes DataSource
        /// </summary>
        public DalObject()
        {
            DataSource.Initialize();
        }

        /// <summary>
        /// Adds a station to the list of base stations
        /// </summary>
        /// <param name="station">The station to add</param>
        /// <returns>true if the station was successfully added, false otherwise</returns>
        public bool AddStation(Station station)
        {
            if (DataSource.Config.CurrentStationsSize < DataSource.stations.Length)
            {
                DataSource.stations[DataSource.Config.CurrentStationsSize++] = station;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a drone to the list of drones
        /// </summary>
        /// <param name="drone">The drone to add</param>
        /// <returns>true if the drone was successfully added, false otherwise</returns>
        public bool AddDrone(Drone drone)
        {
            if (DataSource.Config.CurrentDronesSize < DataSource.drones.Length)
            {
                DataSource.drones[DataSource.Config.CurrentDronesSize++] = drone;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a customer to the list of customers
        /// </summary>
        /// <param name="customer">The customer to add</param>
        /// <returns>true if the customer was successfully added, false otherwise</returns>
        public bool AddCustomer(Customer customer)
        {
            if (DataSource.Config.CurrentCustomersSize < DataSource.customers.Length)
            {
                DataSource.customers[DataSource.Config.CurrentCustomersSize++] = customer;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a new package and adds it to the list of packages
        /// </summary>
        /// <param name="senderId">The ID of the package's sender</param>
        /// <param name="targetId">The ID of the package's target</param>
        /// <param name="weight">The package's weight category</param>
        /// <param name="priority">The package's priority</param>
        /// <returns>true if the package was successfully added, false otherwise</returns>
        public bool AddPackage(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            if (DataSource.Config.CurrentPackagesSize < DataSource.packages.Length)
            {
                DataSource.packages[DataSource.Config.CurrentPackagesSize] = new Package(
                    id: DataSource.Config.CurrentPackagesSize + 1,
                    senderId,
                    targetId,
                    weight,
                    priority,
                    // Use UtcNow instead of Now to avoid portability issues
                    DateTime.UtcNow,
                    0,
                    null,
                    null,
                    null);
                ++DataSource.Config.CurrentPackagesSize;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Assigns a package to a drone
        /// </summary>
        /// <param name="packageId">The ID of the package</param>
        /// <returns>true if the package was successfully assigned to a drone, false otherwise</returns>
        public bool AssignPackage(int packageId)
        {
            var package = GetPackage(packageId);
            //var drone = Array.Find(
            //    DataSource.drones[0..DataSource.Config.CurrentDronesSize],
            //    d => d.Status == DroneStatus.free && d.MaxWeight >= package.Weight);

            // Find a free drone with the minimum required weight, in order to
            // efficiently assign more capable drones.
            Drone? drone = null;
            foreach (var d in DataSource.drones[0..DataSource.Config.CurrentDronesSize])
            {
                if (d.Status == DroneStatus.free)
                {
                    if (d.MaxWeight == package.Weight)
                    {
                        drone = d;
                        break;
                    }

                    if (d.MaxWeight > package.Weight)
                    {
                        // If this is the first available drone, or a better choice
                        // use it.
                        if (drone is null || drone?.MaxWeight > d.MaxWeight)
                        {
                            drone = d;
                        }
                    }
                }
            }

            if (drone is null) return false;

            Drone selectedDrone = drone.Value;

            package.DroneId = selectedDrone.Id;
            package.Scheduled = DateTime.UtcNow;

            selectedDrone.Status = DroneStatus.delivery;

            return true;
        }

        /// <summary>
        /// Collects a package that's been assigned to a drone
        /// </summary>
        /// <param name="packageId">The ID of the package to collect</param>
        public void CollectPackage(int packageId)
        {
            var package = GetPackage(packageId);
            if (package.Scheduled is not null)
                package.PickedUp = DateTime.UtcNow;
        }

        /// <summary>
        /// Provide a package that's been collected by a drone to the customer
        /// </summary>
        /// <param name="packageId">The ID of the package to provide</param>
        public void ProvidePackage(int packageId)
        {
            var package = GetPackage(packageId);
            if (package.PickedUp is not null)
            {
                // Set the delivery time and mark the drone as free
                package.Delivered = DateTime.UtcNow;
                var drone = GetDrone(package.DroneId);
                drone.Status = DroneStatus.free;
            }
        }

        /// <summary>
        /// Sends a drone to a charging station
        /// </summary>
        /// <param name="droneId">The ID of the drone</param>
        /// <param name="stationId">The ID of the station</param>
        public void ChargeDrone(int droneId, int stationId)
        {
            var station = GetStation(stationId);

            if (station.ChargeSlots > 0)
            {
                var droneCharge = new DroneCharge(droneId, stationId);

                DataSource.droneCharges.Add(droneCharge);

                station.ChargeSlots--;
            }
        }

        /// <summary>
        /// Releases a drone from a charging station
        /// </summary>
        /// <param name="droneId">The ID of the drone</param>
        public void ReleaseDrone(int droneId)
        {
            var droneChargeIndex = DataSource.droneCharges.FindIndex(dc => dc.DroneId == droneId);
            var station = GetStation(DataSource.droneCharges[droneChargeIndex].StationId);
            station.ChargeSlots++;
            DataSource.droneCharges.RemoveAt(droneChargeIndex);
        }

        /// <summary>
        /// Gets item from array with ID equal to key
        /// </summary>
        /// <param name="key">Key to search for</param>
        /// <param name="array">Array to search within</param>
        /// <returns>element with ID equal to key. default value if key isn't found</returns>
        private T GetItemByKey<T>(int key, T[] array, int size) where T : IIdentifiable
        {
            // search for element in array with id equal to key
            for (int index = 0; index < size; ++index)
            {
                T element = array[index];

                if (element.Id == key)
                {
                    return element;
                }
            }

            // if an element with id is not found, return default
            return default;
        }

        /// <summary>
        /// gets a station by id from the stations array
        /// </summary>
        /// <param name="id">id of the station</param>
        /// <returns>Station with the given id if found. null otherwise</returns>
        public Station GetStation(int id)
        {
            return GetItemByKey<Station>(id, DataSource.stations, DataSource.Config.CurrentStationsSize);
        }

        /// <summary>
        /// gets a drone by id from the drones array
        /// </summary>
        /// <param name="id">id of the drone</param>
        /// <returns>Drone with the given id if found. null otherwise</returns>
        public Drone GetDrone(int id)
        {
            return GetItemByKey<Drone>(id, DataSource.drones, DataSource.Config.CurrentDronesSize);
        }

        /// <summary>
        /// gets a customer by id from the customers array
        /// </summary>
        /// <param name="id">id of the customer</param>
        /// <returns>Customer with the given id if found. null otherwise</returns>
        public Customer GetCustomer(int id)
        {
            return GetItemByKey<Customer>(id, DataSource.customers, DataSource.Config.CurrentCustomersSize);
        }

        /// <summary>
        /// gets a package by id from the packages array
        /// </summary>
        /// <param name="id">id of the package</param>
        /// <returns>Package with the given id if found. null otherwise</returns>
        public Package GetPackage(int id)
        {
            return GetItemByKey<Package>(id, DataSource.packages, DataSource.Config.CurrentPackagesSize);
        }

        /// <summary>
        /// Creates a string that lists the elements of array
        /// </summary>
        /// <param name="array">Array to create list from</param>
        /// <returns>String that lists the elements of array</returns>
        private string ListArrayItems<T>(T[] array, int size) where T : struct
        {
            // concatenate every element in array into a list
            string result = "";
            for (int index = 0; index < size; ++index)
            {
                T element = array[index];

                result += element.ToString() + "\n";
            }

            return result;
        }

        /// <summary>
        /// Creates a string with the information for every station in the stations list
        /// </summary>
        /// <returns>
        /// string with the information for every station
        /// </returns>
        public string GetStationList()
        {
            return ListArrayItems<Station>(DataSource.stations, DataSource.Config.CurrentStationsSize);
        }

        /// <summary>
        /// Creates a string with the information for every unoccupied station in the stations list
        /// </summary>
        /// <returns>
        /// string with the information for every unoccupied station
        /// </returns>
        public string GetUnoccupiedStationsList()
        {
            return string.Join(
                "\n",
                DataSource.stations[0..DataSource.Config.CurrentStationsSize]
                    .Where(s => s.ChargeSlots > 0));
        }

        /// <summary>
        /// Creates a string with the information for every drone in the drones list
        /// </summary>
        /// <returns>
        /// string with the information for every drone
        /// </returns>
        public string GetDroneList()
        {
            return ListArrayItems<Drone>(DataSource.drones, DataSource.Config.CurrentCustomersSize);
        }

        /// <summary>
        /// Creates a string with the information for every package in the packages list
        /// </summary>
        /// <returns>
        /// string with the information for every package
        /// </returns>
        public string GetPackageList()
        {
            return ListArrayItems<Package>(DataSource.packages, DataSource.Config.CurrentPackagesSize);
        }

        /// <summary>
        /// Creates a string with the information for every unassigned package in the packages list
        /// </summary>
        /// <returns>
        /// string with the information for every unassigned package
        /// </returns>
        public string GetUnassignedPackageList()
        {
            return string.Join(
                "\n",
                DataSource.packages[0..DataSource.Config.CurrentPackagesSize]
                    .Where(p => p.DroneId == 0));
        }

        /// <summary>
        /// Creates a string with the information for every customer in the customers list
        /// </summary>
        /// <returns>
        /// string with the information for every customer
        /// </returns>
        public string GetCustomerList()
        {
            return ListArrayItems<Customer>(DataSource.customers, DataSource.Config.CurrentCustomersSize);
        }
    }
}