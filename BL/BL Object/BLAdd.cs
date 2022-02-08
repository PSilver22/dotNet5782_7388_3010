﻿using System;
using System.Collections.Generic;
using BL;
using DO;
using DalApi;

namespace BlApi
{
    public partial class BL : IBL
    {
        public void AddBaseStation(int id, string name, double latitude, double longitude, int numChargeSlots)
        {
            try
            { dal.AddStation(new(id, name, longitude, latitude, numChargeSlots)); }
            catch (DO.DuplicatedIdException)
            { throw new DuplicatedIdException(id, "base station"); }
        }

        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            try
            { dal.AddCustomer(new(id, name, phone, longitude, latitude)); }
            catch (DO.DuplicatedIdException)
            { throw new DuplicatedIdException(id, "customer"); }
        }

        public void AddDrone(int id, string model, WeightCategory maxWeight, int startingStationId)
        {
            Station station;
            try { station = dal.GetStation(startingStationId); }
            catch (IdNotFoundException)
            { throw new StationNotFoundException(startingStationId); }

            if (station.ChargeSlots == 0)
            { throw new NoAvailableChargingSlotsException(startingStationId); }

            if (maxWeight is < 0 or > WeightCategory.heavy)
            { throw new InvalidWeightException(); }

            try
            {
                var battery = 20.0 + rand.NextDouble() * 20.0;
                dal.AddDrone(new(id, model, maxWeight, battery, station.Longitude, station.Latitude));
            }
            catch (DO.DuplicatedIdException)
            { throw new DuplicatedIdException(id, "drone"); }

            SendDroneToCharge(id);
        }

        public int AddPackage(int senderId, int receiverId, WeightCategory weight, Priority priority)
        {
            try { dal.GetCustomer(senderId); }
            catch (IdNotFoundException)
            { throw new CustomerNotFoundException(senderId); }

            try { dal.GetCustomer(receiverId); }
            catch (IdNotFoundException)
            { throw new CustomerNotFoundException(receiverId); }

            if (weight < 0 || weight > WeightCategory.heavy)
            { throw new InvalidWeightException(); }

            if (priority < 0 || priority > Priority.emergency)
            { throw new InvalidPriorityException(); }

            return dal.AddPackage(senderId, receiverId, weight, priority);
        }
    }
}
