using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DO;

namespace DalXML.Utilities_ {
    internal class DataSourceXml {
        private static string dataSourceFilePath = @"..\..\..\..\DAL\Data\DataSource.xml";
        private static XElement dataSourceRoot;

        /// <summary>
        /// Initializes the data of DataSource
        /// </summary>
        public static void Initialize() {
            // if the file exists, load the data into DataSource
            if (File.Exists(dataSourceFilePath)) {
                dataSourceRoot = XElement.Load(dataSourceFilePath);

                XElement droneListRoot = dataSourceRoot.Element("DroneList");
                XElement stationListRoot = dataSourceRoot.Element("StationList");
                XElement customerListRoot = dataSourceRoot.Element("CustomerList");
                XElement packageListRoot = dataSourceRoot.Element("PackageList");
                XElement droneChargeListRoot = dataSourceRoot.Element("DroneChargeList");

                DalObject.DataSource.drones = (from droneElement in droneListRoot.Elements()
                                              select GetDrone(droneElement)).ToList();

                DalObject.DataSource.stations = (from stationElement in stationListRoot.Elements()
                                                 select GetStation(stationElement)).ToList();

                DalObject.DataSource.customers = (from customerElement in customerListRoot.Elements()
                                                  select GetCustomer(customerElement)).ToList();

                DalObject.DataSource.packages = (from packageElement in packageListRoot.Elements()
                                                 select GetPackage(packageElement)).ToList();
                DalObject.DataSource.droneCharges = (from droneChargeElement in droneChargeListRoot.Elements()
                                                     select GetDroneCharge(droneChargeElement)).ToList();
            } 
            // if the file doesn't exist, initialize DataSource and create the file from the data
            else {
                DalObject.DataSource.Initialize();

                dataSourceRoot = new XElement("DataSource",
                    new XElement("DroneList"),
                    new XElement("StationList"),
                    new XElement("CustomerList"),
                    new XElement("PackageList"),
                    new XElement("DroneChargeList"));

                foreach (var drone in DalObject.DataSource.drones) { AddDrone(drone); }
                foreach (var station in DalObject.DataSource.stations) { AddStation(station); }
                foreach (var package in DalObject.DataSource.packages) { AddPackage(package); }
                foreach (var customer in DalObject.DataSource.customers) { AddCustomer(customer); }
                foreach (var droneCharge in DalObject.DataSource.droneCharges) { AddDroneCharge(droneCharge); }
            }
        }

        /// <summary>
        /// save the current root to the XML file
        /// </summary>
        private static void SaveDataSource() {
            dataSourceRoot.Save(dataSourceFilePath);
        }

        #region Add Methods
        /// <summary>
        /// Adds a drone to the xml file
        /// </summary>
        /// <param name="drone">Drone to add to the xml file</param>
        public static void AddDrone(Drone drone) {
            XElement droneListRoot = dataSourceRoot.Element("DroneList");
            droneListRoot.Add(new XElement("Drone", 
                new XElement("Id", drone.Id),
                new XElement("Model", drone.Model),
                new XElement("Battery", drone.Battery),
                new XElement("MaxWeight", drone.MaxWeight)));

            SaveDataSource();
        }

        /// <summary>
        /// Adds a station to the xml file
        /// </summary>
        /// <param name="station">Station to add to the xml file</param>
        public static void AddStation(Station station) {
            XElement stationListRoot = dataSourceRoot.Element("StationList");
            stationListRoot.Add(new XElement("Station",
                new XElement("Id", station.Id),
                new XElement("Name", station.Name),
                new XElement("Latitude", station.Latitude),
                new XElement("Longitude", station.Longitude),
                new XElement("ChargeSlots", station.ChargeSlots)));

            SaveDataSource();
        }

        /// <summary>
        /// Adds a package to the xml file
        /// </summary>
        /// <param name="package">Package to add to the xml file</param>
        public static void AddPackage(Package package) {
            XElement packageListRoot = dataSourceRoot.Element("PackageList");
            packageListRoot.Add(new XElement("Package",
                new XElement("Id", package.Id),
                new XElement("PickedUp", package.PickedUp),
                new XElement("Priority", package.Priority),
                new XElement("Requested", package.Requested),
                new XElement("Scheduled", package.Scheduled),
                new XElement("SenderId", package.SenderId),
                new XElement("TargetId", package.TargetId),
                new XElement("Weight", package.Weight),
                new XElement("Delivered", package.Delivered),
                new XElement("DroneId", package.DroneId)));

            SaveDataSource();
        }

        /// <summary>
        /// Adds a customer to the xml file
        /// </summary>
        /// <param name="customer">Customer to add to the xml file</param>
        public static void AddCustomer(Customer customer) {
            XElement customerListRoot = dataSourceRoot.Element("CustomerList");
            customerListRoot.Add(new XElement("Customer",
                new XElement("Id", customer.Id),
                new XElement("Name", customer.Name),
                new XElement("Phone", customer.Phone),
                new XElement("Latitude", customer.Latitude),
                new XElement("Longitude", customer.Longitude)));

            SaveDataSource();
        }

        /// <summary>
        /// Adds a drone charge instance to the xml file
        /// </summary>
        /// <param name="droneCharge">Drone charge to add to the xml file</param>
        public static void AddDroneCharge(DroneCharge droneCharge) {
            XElement droneChargeListRoot = dataSourceRoot.Element("DroneChargeList");
            droneChargeListRoot.Add(new XElement("DroneCharge",
                new XElement("StartTime", droneCharge.StartTime),
                new XElement("DroneId", droneCharge.DroneId),
                new XElement("StationId", droneCharge.StationId)));

            SaveDataSource();
        }
        #endregion

        #region Get Methods
        
        /// <summary>
        /// Extracts the Drone object data from an element tag
        /// </summary>
        /// <param name="drone">The drone XML tag to extract the data from</param>
        /// <returns>The Drone saved inside the XML tag.</returns>
        public static Drone GetDrone(XElement drone) {
            return new Drone() {
                Id = Int32.Parse(drone.Element("Id").Value),
                Model = drone.Element("Model").Value,
                Battery = Double.Parse(drone.Element("Battery").Value),
                MaxWeight = (WeightCategory) Enum.Parse(typeof(WeightCategory), drone.Element("MaxWeight").Value)
            };
        }

        /// <summary>
        /// Extracts the Station object data from an element tag
        /// </summary>
        /// <param name="station">The station XML tag to extract the data from</param>
        /// <returns>The Station saved inside the XML tag.</returns>
        public static Station GetStation(XElement station) {
            return new Station() {
                Id = Int32.Parse(station.Element("Id").Value),
                Name = station.Element("Name").Value,
                Longitude = Double.Parse(station.Element("Longitude").Value),
                Latitude = Double.Parse(station.Element("Latitude").Value),
                ChargeSlots = Int32.Parse(station.Element("ChargeSlots").Value)
            };
        }

        /// <summary>
        /// Extracts the Package object data from an element tag
        /// </summary>
        /// <param name="package">The package XML tag to extract the data from</param>
        /// <returns>The Package saved inside the XML tag.</returns>
        public static Package GetPackage(XElement package) {
            return new Package() {
                Id = Int32.Parse(package.Element("Id").Value),
                PickedUp = package.Element("PickedUp").IsEmpty ? null : DateTimeOffset.Parse(package.Element("PickedUp").Value).DateTime,
                Priority = (Priority)Enum.Parse(typeof(Priority), package.Element("Priority").Value),
                Requested = DateTime.Parse(package.Element("Requested").Value),
                Scheduled = package.Element("Scheduled").IsEmpty ? null : DateTimeOffset.Parse(package.Element("Scheduled").Value).DateTime,
                SenderId = Int32.Parse(package.Element("SenderId").Value),
                TargetId = Int32.Parse(package.Element("TargetId").Value),
                Weight = (WeightCategory)Enum.Parse(typeof(WeightCategory), package.Element("Weight").Value),
                Delivered = package.Element("Delivered").IsEmpty ? null : DateTimeOffset.Parse(package.Element("Delivered").Value).DateTime,
                DroneId = package.Element("DroneId").IsEmpty ? null : Int32.Parse(package.Element("DroneId").Value)
            };
        }

        /// <summary>
        /// Extracts the Customer object data from an element tag
        /// </summary>
        /// <param name="customer">The customer XML tag to extract the data from</param>
        /// <returns>The Customer saved inside the XML tag.</returns>
        public static Customer GetCustomer(XElement customer) {
            return new Customer() {
                Id = Int32.Parse(customer.Element("Id").Value),
                Name = customer.Element("Name").Value,
                Phone = customer.Element("Phone").Value,
                Longitude = Double.Parse(customer.Element("Longitude").Value),
                Latitude = Double.Parse(customer.Element("Latitude").Value)
            };
        }

        /// <summary>
        /// Extracts the drone charge object data from an element tag
        /// </summary>
        /// <param name="droneCharge">The DroneCharge XML tag to extract the data from</param>
        /// <returns>The DroneCharge saved inside the XML tag.</returns>
        public static DroneCharge GetDroneCharge(XElement droneCharge) {
            return new DroneCharge() {
                StartTime = DateTimeOffset.Parse(droneCharge.Element("StartTime").Value).DateTime,
                DroneId = Int32.Parse(droneCharge.Element("DroneId").Value),
                StationId = Int32.Parse(droneCharge.Element("StationId").Value)
            };
        }
        #endregion

        #region Set Methods
        /// <summary>
        /// Updates the data of a drone in the xml file
        /// </summary>
        /// <param name="drone">The drone to replace the current in the xml file</param>
        public static void SetDrone(Drone drone) {
            XElement droneListRoot = dataSourceRoot.Element("DroneList");

            XElement droneElement = (from element in droneListRoot.Elements()
                                where Int32.Parse(element.Element("Id").Value) == drone.Id
                                select element).First();

            droneElement.Remove();
            AddDrone(drone);

            SaveDataSource();
        }

        /// <summary>
        /// Updates the data of a station in the xml file
        /// </summary>
        /// <param name="station">The station to replace the current in the xml file</param>
        public static void SetStation(Station station) {
            XElement stationListRoot = dataSourceRoot.Element("StationList");

            XElement stationElement = (from element in stationListRoot.Elements()
                                       where Int32.Parse(element.Element("Id").Value) == station.Id
                                       select element).First();

            stationElement.Remove();
            AddStation(station);

            SaveDataSource();
        }

        /// <summary>
        /// Updates the data of a customer in the xml file
        /// </summary>
        /// <param name="customer">The customer to replace the current in the xml file</param>
        public static void SetCustomer(Customer customer) {
            XElement customerListRoot = dataSourceRoot.Element("CustomerList");

            XElement customerElement = (from element in customerListRoot.Elements()
                                        where Int32.Parse(element.Element("Id").Value) == customer.Id
                                        select element).First();

            customerElement.Remove();
            AddCustomer(customer);

            SaveDataSource();
        }

        /// <summary>
        /// Updates the data of a package in the xml file
        /// </summary>
        /// <param name="package">The package to replace the current in the xml file</param>
        public static void SetPackage(Package package) {
            XElement packageListRoot = dataSourceRoot.Element("PackageList");

            XElement packageElement = (from element in packageListRoot.Elements()
                                       where Int32.Parse(element.Element("Id").Value) == package.Id
                                       select element).First();

            packageElement.Remove();
            AddPackage(package);

            SaveDataSource();
        }
        #endregion
    }
}
