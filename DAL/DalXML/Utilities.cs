using System.Xml.Linq;
using DO;

namespace DalXML
{
    public static class Utilities
    {
        public static XElement ToXElement(this Customer customer)
        {
            return new XElement("customer",
                new XElement("id", customer.Id),
                new XElement("name", customer.Name),
                new XElement("phone", customer.Phone),
                new XElement("longitude", customer.Longitude),
                new XElement("latitude", customer.Latitude)
            );
        }

        public static XElement ToXElement(this DroneCharge droneCharge)
        {
            return new XElement("droneCharge",
                new XElement("stationId", droneCharge.StationId),
                new XElement("droneId", droneCharge.DroneId),
                new XElement("startTime", droneCharge.StartTime)
            );
        }

        public static XElement ToXElement(this Drone drone)
        {
            return new XElement("drone",
                new XElement("id", drone.Id),
                new XElement("model", drone.Model),
                new XElement("maxWeight", drone.MaxWeight),
                new XElement("battery", drone.Battery),
                new XElement("longitude", drone.Longitude),
                new XElement("latitude", drone.Latitude)
            );
        }
        
        public static XElement ToXElement(this Package package)
        {
            var xml = new XElement("package",
                new XElement("id", package.Id),
                new XElement("senderId", package.SenderId),
                new XElement("targetId", package.TargetId),
                new XElement("weight", package.Weight),
                new XElement("priority", package.Priority),
                new XElement("requested", package.Requested)
            );
            if (package.DroneId.HasValue)
                xml.Add(new XElement("droneId", package.DroneId));
            if (package.DroneId.HasValue)
                xml.Add(new XElement("scheduled", package.Scheduled));
            if (package.DroneId.HasValue)
                xml.Add(new XElement("pickedUp", package.PickedUp));
            if (package.DroneId.HasValue)
                xml.Add(new XElement("delivered", package.Delivered));

            return xml;
        }

        public static XElement ToXElement(this Station station)
        {
            return new XElement("station",
                new XElement("id", station.Id),
                new XElement("name", station.Name),
                new XElement("longitude", station.Longitude),
                new XElement("latitude", station.Latitude),
                new XElement("chargeSlots", station.ChargeSlots)
            );
        }
    }
}