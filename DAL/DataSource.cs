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

        private static Random randomGenerator = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Randomly initializes all the arrays in DataSource
        /// </summary>
        public static void Initialize()
        {
            // Set the sizes of all arrays to a random number
            Config.CurrentDronesSize = randomGenerator.Next(5, MaxDrones);
            Config.CurrentStationsSize = randomGenerator.Next(2, MaxStations);
            Config.CurrentCustomersSize = randomGenerator.Next(10, MaxCustomers);
            Config.CurrentPackagesSize = randomGenerator.Next(5, MaxPackages);

            // initialize drone array
            for (int index = 0; index < Config.CurrentDronesSize; ++index)
            {
                // create a randomized drone
                drones[index] = new Drone() 
                {
                    Id = index,
                    Model = "Model " + index,
                    MaxWeight = (WeightCategory) randomGenerator.Next(3),
                    Status = (DroneStatus) randomGenerator.Next(3),
                    Battery = randomGenerator.NextDouble()
                };
            }

            // initialize station array
            for (int index = 0; index < Config.CurrentStationsSize; ++index)
            {
                // create a randomized station
                stations[index] = new Station()
                {
                    Id = index,
                    Name = "Station " + index,
                    Longitude = (randomGenerator.NextDouble() * 360) - 180,
                    Latitude = (randomGenerator.NextDouble() * 180) - 90,
                    ChargeSlots = randomGenerator.Next(1, 11)
                };
            }

            // initialize customer array
            for (int index = 0; index < Config.CurrentCustomersSize; ++index)
            {
                // create a randomized customer
                customers[index] = new Customer()
                {
                    Id = index,
                    Name = "Customer " + index,
                    Phone = (randomGenerator.Next(1000000000)).ToString(),
                    Longitude = (randomGenerator.NextDouble() * 360) - 180,
                    Latitude = (randomGenerator.NextDouble() * 180) - 90
                };
            }

            // initialize package array
            for (int index = 0; index < Config.CurrentPackagesSize; ++index)
            {
                // create a randomized package
                packages[index] = new Package()
                {
                    Id = index
                };
            }
        }

        internal class Config
        {
            // variables with the current size of each of DataSource's arrays
            internal static int CurrentDronesSize = 0;
            internal static int CurrentStationsSize = 0;
            internal static int CurrentCustomersSize = 0;
            internal static int CurrentPackagesSize = 0;
            internal static int NextPackageId = 0;

        }
    }
}