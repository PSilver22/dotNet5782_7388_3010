﻿using DalApi;
using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using DO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DalXML {
    public partial class DalXml : IDAL {
        private T GetItemByKey<T>(int id, List<T> list) where T : IIdentifiable {
            if (id < 0) { throw new InvalidIdException(id); }

            return (from item in list
                    where item.Id == id
                    select item).FirstOrDefault();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int id) {
            return GetItemByKey<Station>(id, DalObject.DataSource.stations);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id) {
            return GetItemByKey<Drone>(id, DalObject.DataSource.drones);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id) {
            return GetItemByKey<Customer>(id, DalObject.DataSource.customers);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Package GetPackage(int id) {
            return GetItemByKey<Package>(id, DalObject.DataSource.packages);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneCharge(int droneId) {
            if (droneId < 0) { throw new InvalidIdException(droneId); }

            return (from item in DalObject.DataSource.droneCharges
            where item.DroneId == droneId
            select item).FirstOrDefault();
        }
    }
}