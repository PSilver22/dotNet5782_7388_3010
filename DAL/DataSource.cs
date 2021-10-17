using System;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// Class containing information about all drones, stations, customers, and packages
        /// </summary>
        public partial class DataSource
        {
            const int MaxDrones = 10;
            const int MaxStations = 5;
            const int MaxCustomers = 100;
            const int MaxPackages = 1000;

            static Drone[] drones;
            static Station[] stations;
            static Customer[] customers;
            static Package[] packages;

            public static void Initialize() { }

            class Config
            {
                // variables with the first open index of each of DataSource's arrays
                static int OpenDronesIndex = 0;
                static int OpenStationsIndex = 0;
                static int OpenCustomersIndex = 0;
                static int OpenPackagesIndex = 0;

            }
        }
    }
}
