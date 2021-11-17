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
            {
                dal.AddStation(new(id, name, longitude, latitude, numChargeSlots));
            }
            catch
            {
                // TODO: throw DuplicatedId
            }
        }

        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            try
            {
                dal.AddCustomer(new(id, name, phone, longitude, latitude));
            }
            catch
            {
                // TODO: throw DuplicatedId
            }
        }

        public void AddDrone(int id, string model, WeightCategory maxWeight, int startingStationId)
        {
            var battery = 20.0 + rand.NextDouble() * 20.0;
            dal.AddDrone(new(id, model, maxWeight, battery));
            IDAL.DO.Station station;
            try
            {
                station = dal.GetStation(startingStationId);
            }
            catch
            {
                // TODO: throw StationIdNotFound
                throw new Exception();
            }
            try
            {
                drones.Add(new(id, model, maxWeight, battery, DroneStatus.maintenance, new(station.Latitude, station.Longitude), null));
            }
            catch
            {
                // TODO: throw DuplicatedId
            }
        }

        public void AddPackage(int senderId, int receiverId, WeightCategory weight, Priority priority)
        {
            try { dal.GetCustomer(senderId); }
            catch
            { /* TODO: throw SenderNotFound */ }
            try { dal.GetCustomer(receiverId); }
            catch
            { /* TODO: throw RecipientNotFound */ }
            dal.AddPackage(senderId, receiverId, weight, priority);
        }
    }
}
