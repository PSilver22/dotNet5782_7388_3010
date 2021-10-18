using System;
using IDAL.DO;

namespace DalObject
{
    /// <summary>
    /// Class containing information about all drones, stations, customers, and packages
    /// </summary>
    public class DataSource
    {
        const int MaxDrones = 10;
        const int MaxStations = 5;
        const int MaxCustomers = 100;
        const int MaxPackages = 1000;

        internal static Drone[] drones = new Drone[MaxDrones];
        internal static Station[] stations = new Station[MaxStations];
        internal static Customer[] customers = new Customer[MaxCustomers];
        internal static Package[] packages = new Package[MaxPackages];

        public static void Initialize() { }

        internal class Config
        {
            // variables with the current size of each of DataSource's arrays
            internal static int CurrentDronesSize = 0;
            internal static int CurrentStationsSize = 0;
            internal static int CurrentCustomersSize = 0;
            internal static int CurrentPackagesSize = 0;

        }
    }
}