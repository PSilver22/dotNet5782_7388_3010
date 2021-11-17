using System;
using System.Collections.Generic;
using IBL.BO;
using IDAL.DO;

namespace IBL
{
    public partial class BL : IBL
    {
        public IEnumerable<BaseStationListing> GetAvailableChargingStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseStationListing> GetBaseStationList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerListing> GetCustomerList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneListing> GetDroneList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PackageListing> GetPackageList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PackageListing> GetUnassignedPackageList()
        {
            throw new NotImplementedException();
        }
    }
}
