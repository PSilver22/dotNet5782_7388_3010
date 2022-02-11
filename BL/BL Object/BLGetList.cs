#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using BL;
using System.Runtime.CompilerServices;

namespace BlApi
{
    public partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationListing> GetBaseStationList(Predicate<BaseStationListing>? filter = null)
        {
            var charging = Dal.GetDroneChargeList();
            return Dal.GetStationList().Select(s =>
            {
                var numOccSlots = charging.Count(c => c.StationId == s.Id);
                return new BaseStationListing(s.Id, s.Name, s.ChargeSlots, numOccSlots);
            }).Where(new Func<BaseStationListing, bool>(filter ?? (x => true))).ToList();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerListing> GetCustomerList(Predicate<CustomerListing>? filter = null)
        {
            var packages = Dal.GetPackageList();
            return Dal.GetCustomerList().Select(c => new CustomerListing(
                    c.Id,
                    c.Name,
                    c.Phone,
                    packages.Count(p => p.SenderId == c.Id && p.Delivered is not null),
                    packages.Count(p => p.SenderId == c.Id && p.Delivered is null),
                    packages.Count(p => p.TargetId == c.Id && p.Delivered is not null),
                    packages.Count(p => p.TargetId == c.Id && p.Delivered is null)))
                .Where(new Func<CustomerListing, bool>(filter ?? (x => true))).ToList();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneListing> GetDroneList(Predicate<DroneListing>? filter = null)
        {
            // return drones.Where(new Func<DroneListing, bool>(filter ?? (x => true))).ToList();
            var result = Dal.GetDroneList().Select(d =>
            {
                var status = DroneStatus.free;

                var packageId = Dal.GetPackageList(p => p.DroneId == d.Id && p.Delivered is null).Select(p => p.Id as int?).FirstOrDefault();
                int? chargingStationId = null;
                if (packageId is not null) status = DroneStatus.delivering;
                else
                {
                    chargingStationId = Dal.GetDroneChargeList(dc => dc.DroneId == d.Id)
                        .Select(dc => dc.StationId as int?).FirstOrDefault();
                    if (chargingStationId.HasValue) status = DroneStatus.maintenance;
                }

                return new DroneListing(
                    d.Id,
                    d.Model,
                    d.MaxWeight,
                    d.Battery,
                    status,
                    new Location(d.Latitude, d.Longitude),
                    packageId,
                    chargingStationId
                );
            });
            return filter is null ? result : result.Where(new Func<DroneListing, bool>(filter));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<PackageListing> GetPackageList(Predicate<PackageListing>? filter = null)
        {
            return Dal.GetPackageList().Select(p => new PackageListing(
                p.Id,
                Dal.GetCustomer(p.SenderId).Name,
                Dal.GetCustomer(p.TargetId).Name,
                p.Weight,
                p.Priority,
                p.Delivered is not null ? PackageStatus.delivered
                : p.PickedUp is not null ? PackageStatus.collected
                : p.Scheduled is not null ? PackageStatus.assigned
                : PackageStatus.created)).Where(new Func<PackageListing, bool>(filter ?? (x => true))).ToList();
        }
    }
}