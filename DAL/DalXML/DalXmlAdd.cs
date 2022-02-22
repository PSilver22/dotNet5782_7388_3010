using DalApi;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using DAL.Exceptions;
using DO;
using System.Runtime.CompilerServices;

namespace DalXML
{
    public partial class DalXml : IDAL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone drone)
        {
            if (drone.Id < 0)
            {
                throw new InvalidIdException(drone.Id);
            }

            if (GetDroneList(d => d.Id == drone.Id).Any())
            {
                throw new DuplicatedIdException(drone.Id, "drone");
            }

            var xml = LoadFile(File.drones);
            xml.Add(drone.ToXElement());
            SaveFile(File.drones, xml);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station station)
        {
            if (station.Id < 0)
            {
                throw new InvalidIdException(station.Id);
            }

            if (DalObject.DataSource.drones.Exists(s => s.Id == station.Id))
            {
                throw new DuplicatedIdException(station.Id, "station");
            }

            var xml = LoadFile(File.stations);
            xml.Add(station.ToXElement());
            SaveFile(File.stations, xml);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddPackage(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            if (senderId < 0 || targetId < 0)
            {
                throw new InvalidIdException((senderId < 0) ? senderId : targetId);
            }

            var package = new Package(
                id: DalObject.DataSource.Config.CurrentPackageId + 1,
                senderId,
                targetId,
                weight,
                priority,
                // Use UtcNow instead of Now to avoid portability issues
                DateTime.UtcNow,
                null,
                null,
                null,
                null);

            try
            {
                var configXml = LoadFile(File.config);
                var currentPackageIdEl = configXml.Element("currentPackageId")!;
                var currentPackageId = int.Parse(currentPackageIdEl.Value) + 1;

                package.Id = currentPackageId;
                var xml = LoadFile(File.packages);
                xml.Add(package.ToXElement());
                SaveFile(File.packages, xml);
            
                currentPackageIdEl.Value = currentPackageId.ToString();
                SaveFile(File.config, configXml);

                return currentPackageId;
            }
            catch
            {
                throw new InvalidXMLException();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer)
        {
            if (customer.Id < 0)
            {
                throw new InvalidIdException(customer.Id);
            }

            if (DalObject.DataSource.customers.Exists(c => c.Id == customer.Id))
            {
                throw new DuplicatedIdException(customer.Id, "customer");
            }

            var xml = LoadFile(File.customers);
            xml.Add(customer.ToXElement());
            SaveFile(File.customers, xml);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(int stationId, int droneId) {
            var newCharge = new DroneCharge(stationId, droneId, DateTime.UtcNow);

            var xml = LoadFile(File.droneCharges);
            xml.Add(newCharge.ToXElement());
            SaveFile(File.droneCharges, xml);
        }
    }
}