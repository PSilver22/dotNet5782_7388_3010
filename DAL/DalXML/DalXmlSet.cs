using DalApi;
using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using DO;
using System.Collections.Generic;

namespace DalXML {
    public partial class DalXml : IDAL {
        public void SetStation(Station station) {
            if (station.Id < 0) {
                throw new InvalidIdException();
            }

            if (!DalObject.DataSource.stations.Any(s => station.Id == s.Id)) {
                throw new IdNotFoundException();
            }

            Utilities.DataSourceXml.SetStation(station);
        }

        public void SetDrone(Drone drone) {
            if (drone.Id < 0) {
                throw new InvalidIdException();
            }

            if (!DalObject.DataSource.stations.Any(d => drone.Id == d.Id)) {
                throw new IdNotFoundException();
            }

            Utilities.DataSourceXml.SetDrone(drone);
        }

        public void SetCustomer(Customer customer) {
            if (customer.Id < 0) {
                throw new InvalidIdException();
            }

            if (!DalObject.DataSource.stations.Any(c => customer.Id == c.Id)) {
                throw new IdNotFoundException();
            }

            Utilities.DataSourceXml.SetCustomer(customer);
        }

        public void SetPackage(Package package) {
            if (package.Id < 0) {
                throw new InvalidIdException();
            }

            if (!DalObject.DataSource.stations.Any(p => package.Id == p.Id)) {
                throw new IdNotFoundException();
            }

            Utilities.DataSourceXml.SetPackage(package);
        }
    }
}
