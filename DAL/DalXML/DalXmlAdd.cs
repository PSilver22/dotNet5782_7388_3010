using DalApi;
using System;
using System.IO;
using System.Xml.Linq;
using DO;
using DalXML.Utilities;

namespace DalXML {
    public partial class DalXml : IDAL {
        public void AddDrone(Drone drone) {
            if (drone.Id < 0) {
                throw new InvalidIdException(drone.Id);
            }

            if (DalObject.DataSource.drones.Exists(d => d.Id == drone.Id)) {
                throw new DuplicatedIdException(drone.Id, "drone");
            }

            DalObject.DataSource.drones.Add(drone);
            DataSourceXml.AddDrone(drone);
        }

        public void AddStation(Station station) {
            if (station.Id < 0) {
                throw new InvalidIdException(station.Id);
            }

            if (DalObject.DataSource.drones.Exists(s => s.Id == station.Id)) {
                throw new DuplicatedIdException(station.Id, "station");
            }

            DalObject.DataSource.stations.Add(station);
            DataSourceXml.AddStation(station);
        }

        public int AddPackage(int senderId, int targetId, WeightCategory weight, Priority priority) {
            if (senderId < 0 || targetId < 0) {
                throw new InvalidIdException((senderId < 0) ? senderId : targetId);
            }

            var newPackage = new Package(
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

            DalObject.DataSource.packages.Add(newPackage);

            DataSourceXml.AddPackage(newPackage);
            return ++DalObject.DataSource.Config.CurrentPackageId;
        }

        public void AddCustomer(Customer customer) {
            if (customer.Id < 0) {
                throw new InvalidIdException(customer.Id);
            }

            if (DalObject.DataSource.customers.Exists(c => c.Id == customer.Id)) {
                throw new DuplicatedIdException(customer.Id, "customer");
            }

            DalObject.DataSource.customers.Add(customer);
            DataSourceXml.AddCustomer(customer);
        }

        public void AddDroneCharge(int stationId, int droneId) {
            var newCharge = new DroneCharge(stationId, droneId, DateTime.UtcNow);

            DalObject.DataSource.droneCharges.Add(newCharge);
            DataSourceXml.AddDroneCharge(newCharge);
        }
    }
}
