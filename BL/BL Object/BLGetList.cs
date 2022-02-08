#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using BL;

namespace BlApi
{
    public partial class BL : IBL
    {
        public IEnumerable<BaseStationListing> GetBaseStationList(Predicate<BaseStationListing>? filter = null)
        {
            var charging = dal.GetDroneChargeList();
            return dal.GetStationList().Select(s =>
            {
                var numOccSlots = charging.Count(c => c.StationId == s.Id);
                return new BaseStationListing(s.Id, s.Name, s.ChargeSlots, numOccSlots);
            }).Where(new Func<BaseStationListing, bool>(filter ?? (x => true))).ToList();
        }

        public IEnumerable<CustomerListing> GetCustomerList(Predicate<CustomerListing>? filter = null)
        {
            var packages = dal.GetPackageList();
            return dal.GetCustomerList().Select(c => new CustomerListing(
                c.Id,
                c.Name,
                c.Phone,
                packages.Count(p => p.SenderId == c.Id && p.Delivered is not null),
                packages.Count(p => p.SenderId == c.Id && p.Delivered is null),
                packages.Count(p => p.TargetId == c.Id && p.Delivered is not null),
                packages.Count(p => p.TargetId == c.Id && p.Delivered is null)))
                .Where(new Func<CustomerListing, bool>(filter ?? (x => true))).ToList();
        }

        public IEnumerable<DroneListing> GetDroneList(Predicate<DroneListing>? filter = null)
        {
            // return drones.Where(new Func<DroneListing, bool>(filter ?? (x => true))).ToList();
            var result = dal.GetDroneList().Select(d =>
            {
                var status = DroneStatus.free;

                var packageId = dal.GetPackageList(p => p.DroneId == d.Id).Select(p => p.Id as int?).FirstOrDefault();
                if (packageId is not null) status = DroneStatus.delivering;
                else if (dal.GetDroneChargeList(dc => dc.DroneId == d.Id).Any())
                    status = DroneStatus.maintenance;

                return new DroneListing(
                    d.Id,
                    d.Model,
                    d.MaxWeight,
                    d.Battery,
                    status,
                    new Location(d.Latitude, d.Longitude),
                    packageId
                );
            });
            return filter is null ? result : result.Where(new Func<DroneListing, bool>(filter));
        }

        public IEnumerable<PackageListing> GetPackageList(Predicate<PackageListing>? filter = null)
        {
            return dal.GetPackageList().Select(p => new PackageListing(
                p.Id,
                dal.GetCustomer(p.SenderId).Name,
                dal.GetCustomer(p.TargetId).Name,
                p.Weight,
                p.Priority,
                p.Delivered is not null ? PackageStatus.delivered
                : p.PickedUp is not null ? PackageStatus.collected
                : p.Scheduled is not null ? PackageStatus.assigned
                : PackageStatus.created)).Where(new Func<PackageListing, bool>(filter ?? (x => true))).ToList();
        }
    }
}
