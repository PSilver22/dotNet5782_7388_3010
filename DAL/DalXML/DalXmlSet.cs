using DalApi;
using System.Linq;
using DO;
using System.Runtime.CompilerServices;

namespace DalXML {
    public partial class DalXml : IDAL {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetStation(Station station) {
            if (station.Id < 0) {
                throw new InvalidIdException();
            }

            if (!DalObject.DataSource.stations.Any(s => station.Id == s.Id)) {
                throw new IdNotFoundException();
            }

            Utilities.DataSourceXml.SetStation(station);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetDrone(Drone drone) {
            if (drone.Id < 0) {
                throw new InvalidIdException();
            }

            if (!DalObject.DataSource.stations.Any(d => drone.Id == d.Id)) {
                throw new IdNotFoundException();
            }

            Utilities.DataSourceXml.SetDrone(drone);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetCustomer(Customer customer) {
            if (customer.Id < 0) {
                throw new InvalidIdException();
            }

            if (!DalObject.DataSource.stations.Any(c => customer.Id == c.Id)) {
                throw new IdNotFoundException();
            }

            Utilities.DataSourceXml.SetCustomer(customer);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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
