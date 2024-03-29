﻿using System;
using System.Collections.Generic;
using BL;
using DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace BlApi
{
    public partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddBaseStation(int id, string name, double latitude, double longitude, int numChargeSlots)
        {
            lock (Dal) {
                try { Dal.AddStation(new(id, name, longitude, latitude, numChargeSlots)); } catch (DO.DuplicatedIdException) { throw new DuplicatedIdException(id, "base station"); }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            lock (Dal) {
                try { Dal.AddCustomer(new(id, name, phone, longitude, latitude)); } catch (DO.DuplicatedIdException) { throw new DuplicatedIdException(id, "customer"); }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, WeightCategory maxWeight, int startingStationId)
        {
            Station station;

            lock (Dal) {
                try { station = Dal.GetStation(startingStationId); } catch (IdNotFoundException) { throw new StationNotFoundException(startingStationId); }

                if (station.ChargeSlots == 0) { throw new NoAvailableChargingSlotsException(startingStationId); }

                if (maxWeight is < 0 or > WeightCategory.heavy) { throw new InvalidWeightException(); }

                try {
                    var battery = 20.0 + _rand.NextDouble() * 20.0;
                    Dal.AddDrone(new(id, model, maxWeight, battery, station.Longitude, station.Latitude));
                } catch (DO.DuplicatedIdException) { throw new DuplicatedIdException(id, "drone"); }

                SendDroneToCharge(id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddPackage(int senderId, int receiverId, WeightCategory weight, Priority priority)
        {
            lock (Dal) {
                try { Dal.GetCustomer(senderId); } catch (IdNotFoundException) { throw new CustomerNotFoundException(senderId); }

                try { Dal.GetCustomer(receiverId); } catch (IdNotFoundException) { throw new CustomerNotFoundException(receiverId); }

                if (weight < 0 || weight > WeightCategory.heavy) { throw new InvalidWeightException(); }

                if (priority < 0 || priority > Priority.emergency) { throw new InvalidPriorityException(); }

                return Dal.AddPackage(senderId, receiverId, weight, priority);
            }
        }
    }
}
