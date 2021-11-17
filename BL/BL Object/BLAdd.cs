using System;
using System.Collections.Generic;
using IBL.BO;
using IDAL.DO;

namespace IBL
{
    public partial class BL : IBL
    {
        public void AddBaseStation(int id, string name, double latitude, double longitude, int numBaseStations)
        {
            throw new NotImplementedException();
        }

        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            throw new NotImplementedException();
        }

        public void AddDrone(int id, string model, WeightCategory maxWeight, int startingStationId)
        {
            throw new NotImplementedException();
        }

        public void AddPackage(int senderId, int receiverId, WeightCategory weight, Priority priority)
        {
            throw new NotImplementedException();
        }
    }
}
