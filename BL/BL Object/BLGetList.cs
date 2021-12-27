#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using BL;

namespace BlApi
{
    public partial class BL : IBL
    {
        public List<BaseStationListing> GetBaseStationList(Predicate<BaseStationListing>? filter = null)
        {
            var charging = dal.GetDroneChargeList();
            return dal.GetStationList().ConvertAll(s =>
            {
                var numOccSlots = charging.Count(c => c.StationId == s.Id);
                return new BaseStationListing(s.Id, s.Name, s.ChargeSlots, numOccSlots);
            }).Where(new Func<BaseStationListing, bool>(filter ?? (x => true))).ToList();
        }

        public List<CustomerListing> GetCustomerList(Predicate<CustomerListing>? filter = null)
        {
            var packages = dal.GetPackageList();
            return dal.GetCustomerList().ConvertAll(c => new CustomerListing(
                c.Id,
                c.Name,
                c.Phone,
                packages.Count(p => p.SenderId == c.Id && p.Delivered is not null),
                packages.Count(p => p.SenderId == c.Id && p.Delivered is null),
                packages.Count(p => p.TargetId == c.Id && p.Delivered is not null),
                packages.Count(p => p.TargetId == c.Id && p.Delivered is null)))
                .Where(new Func<CustomerListing, bool>(filter ?? (x => true))).ToList();
        }

        public List<DroneListing> GetDroneList(Predicate<DroneListing>? filter = null)
        {
            return drones.Where(new Func<DroneListing, bool>(filter ?? (x => true))).ToList();
        }

        public List<PackageListing> GetPackageList(Predicate<PackageListing>? filter = null)
        {
            return dal.GetPackageList().ConvertAll(p => new PackageListing(
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
