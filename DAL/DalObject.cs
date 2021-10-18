namespace IDAL
{
	namespace DO
	{
		public partial class DataSource
		{
			/// <summary>
			/// A class that interacts with and performs operations
			/// on the data source class
			/// </summary>
			public class DalObject
			{
                /// <summary>
                /// Gets item from array with ID equal to key
                /// </summary>
                /// <param name="key">Key to search for</param>
                /// <param name="array">Array to search within</param>
                /// <returns>element with ID equal to key. default value if key isn't found</returns>
                private T GetItemByKey<T>(int key, T[] array) where T : IIdentifiable
                {
                    // search for element in array with id equal to key
                    foreach (T element in array)
                    {
                        if (element.Id == key)
                        {
                            return element;
                        }
                    }

                    // if an element with id is not found, return default
                    return default(T);
                }

                /// <summary>
                /// gets a station by id from the stations array
                /// </summary>
                /// <param name="id">id of the station</param>
                /// <returns>Station with the given id if found. null otherwise</returns>
                public Station GetStation(int id)
                {
                    return GetItemByKey<Station>(id, stations);
                }

                /// <summary>
                /// gets a drone by id from the drones array
                /// </summary>
                /// <param name="id">id of the drone</param>
                /// <returns>Drone with the given id if found. null otherwise</returns>
                public Drone GetDrone(int id)
                {
                    return GetItemByKey<Drone>(id, drones);
                }

                /// <summary>
                /// gets a customer by id from the customers array
                /// </summary>
                /// <param name="id">id of the customer</param>
                /// <returns>Customer with the given id if found. null otherwise</returns>
                public Customer GetCustomer(int id)
                {
                    return GetItemByKey<Customer>(id, customers);
                }

                /// <summary>
                /// gets a package by id from the packages array
                /// </summary>
                /// <param name="id">id of the package</param>
                /// <returns>Package with the given id if found. null otherwise</returns>
                public Package GetPackage(int id)
                {
                    return GetItemByKey<Package>(id, packages);
                }

                /// <summary>
                /// Creates a string that lists the elements of array
                /// </summary>
                /// <param name="array">Array to create list from</param>
                /// <returns>String that lists the elements of array</returns>
                private string ListArrayItems<T>(T[] array) where T : struct
                {
                    // concatenate every element in array into a list
                    string result = "";
                    foreach (T element in array)
                    {
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
                    return ListArrayItems<Station>(stations);
                }

                /// <summary>
                /// Creates a string with the information for every drone in the drones list
                /// </summary>
                /// <returns>
                /// string with the information for every drone
                /// </returns>
                public string GetDroneList()
                {
                    return ListArrayItems<Drone>(drones);
                }

                /// <summary>
                /// Creates a string with the information for every package in the packages list
                /// </summary>
                /// <returns>
                /// string with the information for every package
                /// </returns>
                public string GetPackageList()
                {
                    return ListArrayItems<Package>(packages);
                }

                /// <summary>
                /// Creates a string with the information for every customer in the customers list
                /// </summary>
                /// <returns>
                /// string with the information for every customer
                /// </returns>
                public string GetCustomerList()
                {
                    return ListArrayItems<Customer>(customers);
                }
            }
		}
	}
}
