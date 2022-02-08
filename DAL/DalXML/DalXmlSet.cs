using DalApi;
using System.Linq;
using DO;
using System.Collections.Generic;
using DAL.Exceptions;
using System.Runtime.CompilerServices;
using System;

namespace DalXML {
    public partial class DalXml : IDAL {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetStation(Station station) {
            if (station.Id < 0) {
                throw new InvalidIdException();
            }

            try
            {
                var xml = LoadFile(File.stations);
                
                try
                {
                    xml.Elements().First(e => e.Element("id")!.Value == station.Id.ToString()).Remove();
                }
                catch (InvalidOperationException)
                {
                    throw new IdNotFoundException(station.Id);
                }

                xml.Add(station.ToXElement());
                SaveFile(File.stations, xml);
            }
            catch (Exception e)
            {
                if (e is IdNotFoundException) throw;
                throw new InvalidXMLException();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetDrone(Drone drone) {
            if (drone.Id < 0) {
                throw new InvalidIdException();
            }

            try
            {
                var xml = LoadFile(File.drones);
                
                try
                {
                    xml.Elements().First(e => e.Element("id")!.Value == drone.Id.ToString()).Remove();
                }
                catch (InvalidOperationException)
                {
                    throw new IdNotFoundException(drone.Id);
                }

                xml.Add(drone.ToXElement());
                SaveFile(File.drones, xml);
            }
            catch (Exception e)
            {
                if (e is IdNotFoundException) throw;
                throw new InvalidXMLException();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetCustomer(Customer customer) {
            if (customer.Id < 0) {
                throw new InvalidIdException();
            }

            try
            {
                var xml = LoadFile(File.customers);
                
                try
                {
                    xml.Elements().First(e => e.Element("id")!.Value == customer.Id.ToString()).Remove();
                }
                catch (InvalidOperationException)
                {
                    throw new IdNotFoundException(customer.Id);
                }

                xml.Add(customer.ToXElement());
                SaveFile(File.customers, xml);
            }
            catch (Exception e)
            {
                if (e is IdNotFoundException) throw;
                throw new InvalidXMLException();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetPackage(Package package) {
            if (package.Id < 0) {
                throw new InvalidIdException();
            }

            try
            {
                var xml = LoadFile(File.packages);
                
                try
                {
                    xml.Elements().First(e => e.Element("id")!.Value == package.Id.ToString()).Remove();
                }
                catch (InvalidOperationException)
                {
                    throw new IdNotFoundException(package.Id);
                }

                xml.Add(package.ToXElement());
                SaveFile(File.packages, xml);
            }
            catch (Exception e)
            {
                if (e is IdNotFoundException) throw;
                throw new InvalidXMLException();
            }
        }
    }
}
