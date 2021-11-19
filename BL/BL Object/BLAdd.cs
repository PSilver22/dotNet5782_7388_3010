using System;
using System.Collections.Generic;
using IBL.BO;
using IDAL.DO;

namespace IBL
{
    public partial class BL : IBL
    {
        public void AddBaseStation(int id, string name, double latitude, double longitude, int numChargeSlots)
        {
            try
            { dal.AddStation(new(id, name, longitude, latitude, numChargeSlots)); }
            catch (IDAL.DO.DuplicatedIdException)
            { throw new DuplicatedIdException(id, "base station"); }
        }

        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            try
            { dal.AddCustomer(new(id, name, phone, longitude, latitude)); }
            catch (IDAL.DO.DuplicatedIdException)
            { throw new DuplicatedIdException(id, "customer"); }
        }

        public void AddDrone(int id, string model, WeightCategory maxWeight, int startingStationId)
        {
            var battery = 20.0 + rand.NextDouble() * 20.0;
            dal.AddDrone(new(id, model, maxWeight, battery));

            Station station;
            try { station = dal.GetStation(startingStationId); }
            catch (IdNotFoundException)
            { throw new StationNotFoundException(startingStationId); }

            try
            {
                drones.Add(new(id, model, maxWeight, battery, DroneStatus.maintenance, new(station.Latitude, station.Longitude), null));
            }
            catch (IDAL.DO.DuplicatedIdException)
            { throw new DuplicatedIdException(id, "drone"); }
        }

        public void AddPackage(int senderId, int receiverId, WeightCategory weight, Priority priority)
        {
            try { dal.GetCustomer(senderId); }
            catch (IdNotFoundException)
            { throw new CustomerNotFoundException(senderId); }

            try { dal.GetCustomer(receiverId); }
            catch (IdNotFoundException)
            { throw new CustomerNotFoundException(receiverId); }

            dal.AddPackage(senderId, receiverId, weight, priority);
        }
    }
}
