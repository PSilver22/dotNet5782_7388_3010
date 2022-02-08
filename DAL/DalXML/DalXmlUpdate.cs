#nullable enable

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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int id, string? name = null, double? longitude = null, double? latitude = null,
            int? chargeSlots = null)
        {
            var station = GetStation(id);
            var xml = LoadFile(File.stations);
            xml.Elements().First(e => e.Element("id")!.Value == id.ToString()).Remove();

            if (name is not null) station.Name = name;
            if (longitude is not null) station.Longitude = longitude.Value;
            if (latitude is not null) station.Latitude = latitude.Value;
            if (chargeSlots is not null) station.ChargeSlots = chargeSlots.Value;

            xml.Add(station.ToXElement());

            SaveFile(File.stations, xml);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int id, string? model = null, WeightCategory? maxWeight = null, double? battery = null,
            double? longitude = null, double? latitude = null)
        {
            var drone = GetDrone(id);
            var xml = LoadFile(File.drones);
            xml.Elements().First(e => e.Element("id")!.Value == id.ToString()).Remove();

            if (model is not null) drone.Model = model;
            if (maxWeight is not null) drone.MaxWeight = maxWeight.Value;
            if (battery is not null) drone.Battery = battery.Value;
            if (longitude is not null) drone.Longitude = longitude.Value;
            if (latitude is not null) drone.Latitude = latitude.Value;

            xml.Add(drone.ToXElement());

            SaveFile(File.drones, xml);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int id, string? name = null, string? phone = null, double? longitude = null,
            double? latitude = null)
        {
            var customer = GetCustomer(id);
            var xml = LoadFile(File.customers);
            xml.Elements().First(e => e.Element("id")!.Value == id.ToString()).Remove();

            if (name is not null) customer.Name = name;
            if (phone is not null) customer.Phone = phone;
            if (longitude is not null) customer.Longitude = longitude.Value;
            if (latitude is not null) customer.Latitude = latitude.Value;

            xml.Add(customer.ToXElement());

            SaveFile(File.customers, xml);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdatePackage(int id, int? senderId = null, int? targetId = null, WeightCategory? weight = null,
            Priority? priority = null, DateTime? requested = null, int? droneId = null, DateTime? scheduled = null,
            DateTime? pickedUp = null, DateTime? delivered = null)
        {
            var package = GetPackage(id);
            var xml = LoadFile(File.packages);
            xml.Elements().First(e => e.Element("id")!.Value == id.ToString()).Remove();

            if (senderId is not null) package.SenderId = senderId.Value;
            if (targetId is not null) package.TargetId = targetId.Value;
            if (weight is not null) package.Weight = weight.Value;
            if (priority is not null) package.Priority = priority.Value;
            if (requested is not null) package.Requested = requested.Value;
            if (droneId is not null) package.DroneId = droneId;
            if (scheduled is not null) package.Scheduled = scheduled;
            if (pickedUp is not null) package.PickedUp = pickedUp;
            if (delivered is not null) package.Delivered = delivered;

            xml.Add(package.ToXElement());

            SaveFile(File.packages, xml);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDroneCharge(int droneId)
        {
            if (droneId < 0)
            {
                throw new InvalidIdException();
            }

            try
            {
                var xml = LoadFile(File.droneCharges);

                try
                {
                    xml.Elements().First(e => e.Element("droneId")!.Value == droneId.ToString()).Remove();
                }
                catch (InvalidOperationException)
                {
                    throw new IdNotFoundException(droneId);
                }

                SaveFile(File.droneCharges, xml);
            }
            catch (Exception e)
            {
                if (e is IdNotFoundException) throw;
                throw new InvalidXMLException();
            }
        }
    }
}