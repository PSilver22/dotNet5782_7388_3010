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
        /// <returns>The new package's ID</returns>
        public int AddPackage(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            if (senderId < 0 || targetId < 0)
            {
                throw new InvalidIdException((senderId < 0) ? senderId : targetId);
            }

            if (DataSource.packages.Count >= DataSource.MaxPackages)
            {
                throw new MaximumCapacityException("Package list is at max capacity.");
            }

            DataSource.packages.Add(new Package(
                    id: DataSource.Config.CurrentPackageId + 1,
                    senderId,
                    targetId,
                    weight,
                    priority,
                    // Use UtcNow instead of Now to avoid portability issues
                    DateTime.UtcNow,
                    null,
                    null,
                    null,
                    null));
            return ++DataSource.Config.CurrentPackageId;
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
        /// Returns a list with the information for every package in the packages list
        /// </summary>
        /// <returns>
        /// Packages list
        /// </returns>
        public List<Package> GetPackageList()
        {
            return DataSource.packages;
        }

        /// <summary>
        /// Creates a string with the information for every unassigned package in the packages list
        /// </summary>
        /// <returns>
        /// string with the information for every unassigned package
        /// </returns>
        public List<Package> GetUnassignedPackageList()
        {
            return (List<Package>) DataSource.packages.Where(x => x.DroneId == 0);
        }

        /// <summary>
        /// Update the given package properties for the package with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        /// <param name="requested"></param>
        /// <param name="droneId"></param>
        /// <param name="scheduled"></param>
        /// <param name="pickedUp"></param>
        /// <param name="delivered"></param>
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
            DateTime? delivered = null)
        {
            int index = GetPackageIndex(id);

            Package updatedPackage = DataSource.packages[index];

            updatedPackage.SenderId = senderId ?? updatedPackage.SenderId;
            updatedPackage.TargetId = targetId ?? updatedPackage.TargetId;
            updatedPackage.Weight = weight ?? updatedPackage.Weight;
            updatedPackage.Priority = priority ?? updatedPackage.Priority;
            updatedPackage.Requested = requested ?? updatedPackage.Requested;
            updatedPackage.DroneId = droneId ?? updatedPackage.DroneId;
            updatedPackage.Scheduled = scheduled ?? updatedPackage.Scheduled;
            updatedPackage.PickedUp = pickedUp ?? updatedPackage.PickedUp;
            updatedPackage.Delivered = delivered ?? updatedPackage.Delivered;

            SetPackage(updatedPackage);
        }

        /// <summary>
        /// Sets the package with matching id to the given package
        /// </summary>
        /// <param name="package"></param>
        public void SetPackage(Package package)
        {
            int index = GetPackageIndex(package.Id);

            DataSource.packages[index] = package;
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
