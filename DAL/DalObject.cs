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
        public void AddStation(Station station)
        {
            if (DataSource.Config.CurrentStationsSize < DataSource.stations.Length)
            {
                DataSource.stations[DataSource.Config.CurrentStationsSize++] = station;
            }
        }

        /// <summary>
        /// Adds a drone to the list of drones
        /// </summary>
        /// <param name="drone">The station to add</param>
        public void AddDrone(Drone drone)
        {
            if (DataSource.Config.CurrentDronesSize < DataSource.drones.Length)
            {
                DataSource.drones[DataSource.Config.CurrentDronesSize++] = drone;
            }
        }

        /// <summary>
        /// Adds a customer to the list of customers
        /// </summary>
        /// <param name="customer">The station to add</param>
        public void AddCustomer(Customer customer)
        {
            if (DataSource.Config.CurrentCustomersSize < DataSource.customers.Length)
            {
                DataSource.customers[DataSource.Config.CurrentCustomersSize++] = customer;
            }
        }

        /// <summary>
        /// Adds a station to the list of base stations
        /// </summary>
        /// <param name="package">The station to add</param>
        public void AddPackage(Package package)
        {
            if (DataSource.Config.CurrentPackagesSize < DataSource.packages.Length)
            {
                DataSource.packages[DataSource.Config.CurrentPackagesSize++] = package;
            }
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