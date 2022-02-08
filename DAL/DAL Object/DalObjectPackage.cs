#nullable enable

using System;
using System.Linq;
using System.Collections.Generic;
using DO;
using DalApi;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddPackage(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            if (senderId < 0 || targetId < 0)
            {
                throw new InvalidIdException((senderId < 0) ? senderId : targetId);
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Package GetPackage(int id)
        {
            return GetItemByKey<Package>(id, DataSource.packages);
        }

        /// <summary>
        /// Returns a list with the information for every package in the packages list
        /// </summary>
        /// <param name="filter">The filter applied to the objects in the list</param>
        /// <returns>
        /// Packages list
        /// </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<Package> GetPackageList(Predicate<Package>? filter = null)
        {
            return DataSource.packages.Where(new Func<Package, bool>(filter ?? (x => true))).ToList();
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        private int GetPackageIndex(int id)
        {
            return GetItemIndexByKey<Package>(id, DataSource.packages);
        }
    }
}
