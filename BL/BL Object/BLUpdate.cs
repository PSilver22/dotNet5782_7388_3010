#nullable enable

using System;
using System.Collections.Generic;
using IBL.BO;
using IDAL.DO;

namespace IBL
{
    public partial class BL : IBL
    {
        public void UpdateBaseStation(int id, string? name = null, int? numChargingStations = null)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(int id, string? name = null, string? phone = null)
        {
            throw new NotImplementedException();
        }

        public void UpdateDrone(int id, string model)
        {
            throw new NotImplementedException();
        }
    }
}
