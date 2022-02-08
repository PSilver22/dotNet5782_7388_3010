#nullable enable

using DalApi;
using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using DO;
using System.Collections.Generic;
using DAL.Exceptions;

namespace DalXML
{
    public partial class DalXml : IDAL
    {
        public IEnumerable<Station> GetStationList(Predicate<Station>? filter = null)
        {
            try
            {
                var result = LoadFile(File.stations).Elements().Select(e => new Station(
                    int.Parse(e.Element("id")!.Value),
                    e.Element("name")!.Value,
                    double.Parse(e.Element("longitude")!.Value),
                    double.Parse(e.Element("latitude")!.Value),
                    int.Parse(e.Element("chargeSlots")!.Value)
                ));
                return filter is null ? result : result.Where(new Func<Station, bool>(filter));
            }
            catch
            {
                throw new InvalidXMLException();
            }
        }

        public IEnumerable<Drone> GetDroneList(Predicate<Drone>? filter = null)
        {
            try
            {
                var result = LoadFile(File.drones).Elements().Select(e => new Drone(
                    int.Parse(e.Element("id")!.Value),
                    e.Element("model")!.Value,
                    Enum.TryParse(e.Element("maxWeight")!.Value, out WeightCategory maxWeight)
                        ? maxWeight
                        : WeightCategory.light,
                    double.Parse(e.Element("battery")!.Value),
                    double.Parse(e.Element("longitude")!.Value),
                    double.Parse(e.Element("latitude")!.Value)
                ));
                return filter is null ? result : result.Where(new Func<Drone, bool>(filter));
            }
            catch
            {
                throw new InvalidXMLException();
            }
        }

        public IEnumerable<Package> GetPackageList(Predicate<Package>? filter = null)
        {
            try
            {
                var result = LoadFile(File.packages).Elements().Select(e => new Package(
                    int.Parse(e.Element("id")!.Value),
                    int.Parse(e.Element("senderId")!.Value),
                    int.Parse(e.Element("targetId")!.Value),
                    Enum.TryParse(e.Element("weight")!.Value, out WeightCategory weight)
                        ? weight
                        : WeightCategory.light,
                    Enum.TryParse(e.Element("priority")!.Value, out Priority priority) ? priority : Priority.regular,
                    DateTime.Parse(e.Element("requested")!.Value).ToUniversalTime(),
                    int.TryParse(e.Element("droneId")?.Value ?? "", out var droneId) ? droneId : null,
                    DateTime.TryParse(e.Element("scheduled")?.Value ?? "", out DateTime scheduled) ? scheduled.ToUniversalTime() : null,
                    DateTime.TryParse(e.Element("pickedUp")?.Value ?? "", out DateTime pickedUp) ? pickedUp.ToUniversalTime() : null,
                    DateTime.TryParse(e.Element("delivered")?.Value ?? "", out DateTime delivered) ? delivered.ToUniversalTime() : null
                ));
                return filter is null ? result : result.Where(new Func<Package, bool>(filter));
            }
            catch
            {
                throw new InvalidXMLException();
            }
        }

        public IEnumerable<Customer> GetCustomerList(Predicate<Customer>? filter = null)
        {
            try
            {
                var result = LoadFile(File.customers).Elements().Select(e => new Customer(
                    int.Parse(e.Element("id")!.Value),
                    e.Element("name")!.Value,
                    e.Element("phone")!.Value,
                    double.Parse(e.Element("longitude")!.Value),
                    double.Parse(e.Element("latitude")!.Value)
                ));
                return filter is null ? result : result.Where(new Func<Customer, bool>(filter));
            }
            catch
            {
                throw new InvalidXMLException();
            }
        }

        public IEnumerable<DroneCharge> GetDroneChargeList(Predicate<DroneCharge>? filter = null)
        {
            try
            {
                var result = LoadFile(File.droneCharges).Elements().Select(e => new DroneCharge(
                    int.Parse(e.Element("stationId")!.Value),
                    int.Parse(e.Element("droneId")!.Value),
                    DateTime.Parse(e.Element("startTime")!.Value).ToUniversalTime()
                ));
                return filter is null ? result : result.Where(new Func<DroneCharge, bool>(filter));
            }
            catch
            {
                throw new InvalidXMLException();
            }
        }
    }
}