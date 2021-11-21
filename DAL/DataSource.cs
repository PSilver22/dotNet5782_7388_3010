using System;
using System.Collections.Generic;
using IDAL.DO;

namespace DalObject
{
    /// <summary>
    /// Class containing information about all drones, stations, customers, and packages
    /// </summary>
    public class DataSource
    {
        internal const int MaxDrones = 10;
        internal const int MaxStations = 5;
        internal const int MaxCustomers = 100;
        internal const int MaxPackages = 1000;

        internal static List<Drone> drones = new();
        internal static List<Station> stations = new();
        internal static List<Customer> customers = new();
        internal static List<Package> packages = new();

        internal static List<DroneCharge> droneCharges = new();

        private static Random randomGenerator = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Randomly initializes all the arrays in DataSource
        /// </summary>
        public static void Initialize()
        {
            // Set the sizes of all arrays to a random number
            int dronesSize = randomGenerator.Next(5, MaxDrones);
            int stationsSize = randomGenerator.Next(2, MaxStations);
            int customersSize = randomGenerator.Next(10, MaxCustomers);
            int packagesSize = randomGenerator.Next(5, MaxPackages);

            // initialize drone array
            for (int count = 0; count < dronesSize; ++count)
            {
                // create a randomized drone
                drones.Add(new Drone()
                {
                    Id = count,
                    Model = "Model " + count,
                    MaxWeight = (WeightCategory)randomGenerator.Next(3),
                    Battery = randomGenerator.NextDouble()
                });
            }

            // initialize station array
            for (int count = 0; count < stationsSize; ++count)
            {
                // create a randomized station
                stations.Add(new Station()
                {
                    Id = count,
                    Name = "Station " + count,
                    Longitude = (randomGenerator.NextDouble() * 360) - 180,
                    Latitude = (randomGenerator.NextDouble() * 180) - 90,
                    ChargeSlots = randomGenerator.Next(1, 11)
                });
            }

            // initialize customer array
            for (int count = 0; count < customersSize; ++count)
            {
                // create a randomized customer
                customers.Add(new Customer()
                {
                    Id = count,
                    Name = "Customer " + count,
                    Phone = (randomGenerator.Next(1000000000)).ToString(),
                    Longitude = (randomGenerator.NextDouble() * 360) - 180,
                    Latitude = (randomGenerator.NextDouble() * 180) - 90
                });
            }

            // initialize package array
            for (int count = 0; count < packagesSize; ++count)
            {
                // create a randomized package
                packages.Add(new Package()
                {
                    Id = count,
                    SenderId = customers[randomGenerator.Next(customers.Count)].Id,
                    TargetId = customers[randomGenerator.Next(customers.Count)].Id,
                    Requested = DateTime.UtcNow,
                    Weight = (WeightCategory) randomGenerator.Next(3),
                    Priority = (Priority) randomGenerator.Next(3)
                });
            }

            Config.CurrentPackageId = packagesSize;
        }

        internal class Config
        {
            internal static int CurrentPackageId = 0;

            internal static readonly double free = 1;
            internal static readonly double lightWeight = 2;
            internal static readonly double midWeight = 3;
            internal static readonly double heavyWeight = 4;

            internal static readonly double chargeRate = 20;
        }
    }
}