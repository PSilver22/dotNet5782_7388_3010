using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        public List<BaseStationListing> GetAvailableChargingStations()
        {
            var charging = dal.GetDroneChargeList();
            return dal.GetUnoccupiedStationsList().ConvertAll(s =>
            {
                var numOccSlots = charging.Count(c => c.StationId == s.Id);
                return new BaseStationListing(s.Id, s.Name, s.ChargeSlots, numOccSlots);
            });
        }

        public List<BaseStationListing> GetBaseStationList()
        {
            var charging = dal.GetDroneChargeList();
            return dal.GetStationList().ConvertAll(s =>
            {
                var numOccSlots = charging.Count(c => c.StationId == s.Id);
                return new BaseStationListing(s.Id, s.Name, s.ChargeSlots, numOccSlots);
            });
        }

        public List<CustomerListing> GetCustomerList()
        {
            var packages = dal.GetPackageList();
            return dal.GetCustomerList().ConvertAll(c => new CustomerListing(
                c.Id,
                c.Name,
                c.Phone,
                packages.Count(p => p.SenderId == c.Id && p.Delivered is not null),
                packages.Count(p => p.SenderId == c.Id && p.Delivered is null),
                packages.Count(p => p.TargetId == c.Id && p.Delivered is not null),
                packages.Count(p => p.TargetId == c.Id && p.Delivered is null)));
        }

        public List<DroneListing> GetDroneList()
        {
            return drones;
        }

        public List<PackageListing> GetPackageList()
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
                : PackageStatus.created));
        }

        public List<PackageListing> GetUnassignedPackageList()
        {
            return dal.GetUnassignedPackageList().ConvertAll(p => new PackageListing(
                p.Id,
                dal.GetCustomer(p.SenderId).Name,
                dal.GetCustomer(p.TargetId).Name,
                p.Weight,
                p.Priority,
                p.Delivered is not null ? PackageStatus.delivered
                : p.PickedUp is not null ? PackageStatus.collected
                : p.Scheduled is not null ? PackageStatus.assigned
                : PackageStatus.created));
        }
    }
}
