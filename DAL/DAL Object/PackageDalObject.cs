using System;
using System.Linq;
using System.Collections.Generic;
using IDAL.DO;

namespace DalObject
{
	public partial class DalObject
	{
        /// <summary>
        /// Creates a new package and adds it to the list of packages
        /// </summary>
        /// <param name="senderId">The ID of the package's sender</param>
        /// <param name="targetId">The ID of the package's target</param>
        /// <param name="weight">The package's weight category</param>
        /// <param name="priority">The package's priority</param>
        /// <returns>true if the package was successfully added, false otherwise</returns>
        public void AddPackage(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            if (senderId < 0 || targetId < 0)
            {
                throw new InvalidIdException((senderId < 0) ? senderId : targetId);
            }

            if (DataSource.packages.Count < DataSource.MaxPackages)
            {
                DataSource.packages.Add(new Package(
                    id: DataSource.Config.CurrentPackageId + 1,
                    senderId,
                    targetId,
                    weight,
                    priority,
                    // Use UtcNow instead of Now to avoid portability issues
                    DateTime.UtcNow,
                    0,
                    null,
                    null,
                    null));
                ++DataSource.Config.CurrentPackageId;
            }

            throw new MaximumCapacityException("Package list is at max capacity.");
        }

        /// <summary>
        /// gets a package by id from the packages array
        /// </summary>
        /// <param name="id">id of the package</param>
        /// <returns>Package with the given id if found. null otherwise</returns>
        public Package GetPackage(int id)
        {
            return GetItemByKey<Package>(id, DataSource.packages);
        }

        /// <summary>
        /// Creates a string with the information for every package in the packages list
        /// </summary>
        /// <returns>
        /// string with the information for every package
        /// </returns>
        public string GetPackageList()
        {
            return ListItems<Package>(DataSource.packages);
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
                DataSource.packages.GetRange(0, DataSource.packages.Count)
                    .Where(p => p.DroneId == 0));
        }

        /// <summary>
        /// gets a package's index by id from the packages array
        /// </summary>
        /// <param name="id">id of the package</param>
        /// <returns>Index of the package with the given id if found.</returns>
        private int GetPackageIndex(int id)
        {
            return GetItemIndexByKey<Package>(id, DataSource.packages);
        }
    }
}
