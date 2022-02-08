using DalApi;
using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using DO;
using System.Collections.Generic;
using DAL.Exceptions;
using System.Runtime.CompilerServices;

namespace DalXML
{
    public partial class DalXml : IDAL
    {
        private static T GetItemByKey<T>(int id, IEnumerable<T> list) where T : IIdentifiable
        {
            if (id < 0)
            {
                throw new InvalidIdException(id);
            }

            try
            {
                return list.First(e => e.Id == id);
            }
            catch
            {
                throw new IdNotFoundException(id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int id)
        {
            return GetItemByKey(id, GetStationList());
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            return GetItemByKey(id, GetDroneList());
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            return GetItemByKey(id, GetCustomerList());
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Package GetPackage(int id)
        {
            return GetItemByKey(id, GetPackageList());
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneCharge(int droneId)
        {
            return GetItemByKey(droneId, GetDroneChargeList());
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public (double Free, double LightWeight, double MidWeight, double HeavyWeight, double ChargeRate)
            GetPowerConsumption()
        {
            var config = LoadFile(File.config);
            try
            {
                var powerConsumption = config.Element("powerConsumption")!;
                return (
                    int.Parse(powerConsumption.Element("free")!.Value),
                    int.Parse(powerConsumption.Element("light")!.Value),
                    int.Parse(powerConsumption.Element("mid")!.Value),
                    int.Parse(powerConsumption.Element("heavy")!.Value),
                    int.Parse(config.Element("chargeRate")!.Value));
            }
            catch
            {
                throw new InvalidXMLException();
            }
        }
    }
}