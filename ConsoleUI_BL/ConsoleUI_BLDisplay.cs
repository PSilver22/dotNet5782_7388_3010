using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace ConsoleUI_BL
{
	partial class ConsoleUI
	{
		/// <summary>
		/// Get an id and display the base station with that id
		/// </summary>
		/// <param name="logicLayer">The instance of the logic layer</param>
		internal static void DisplayBaseStation(IBL.IBL logicLayer)
		{
			Console.WriteLine("Input station ID: ");
			int id = Utils.ReadInt();

			Console.WriteLine(logicLayer.GetBaseStation(id).ToString());
		}

		/// <summary>
		/// Get an id and display the drone with that id
		/// </summary>
		/// <param name="logicLayer">The instance of the logic layer</param>
		internal static void DisplayDrone(IBL.IBL logicLayer)
		{
			Console.WriteLine("Input drone ID: ");
			int id = Utils.ReadInt();

			Console.WriteLine(logicLayer.GetDrone(id).ToString());
		}

		/// <summary>
		/// Get an id and display the customer with that id
		/// </summary>
		/// <param name="logicLayer">The instance of the logic layer</param>
		internal static void DisplayCustomer(IBL.IBL logicLayer)
		{
			Console.WriteLine("Input customer ID: ");
			int id = Utils.ReadInt();

			Console.WriteLine(logicLayer.GetCustomer(id).ToString());
		}

		/// <summary>
		/// Get an id and display the package with that id
		/// </summary>
		/// <param name="logicLayer">The instance of the logic layer</param>
		internal static void DisplayPackage(IBL.IBL logicLayer)
		{
			Console.WriteLine("Input package ID: ");
			int id = Utils.ReadInt();

			Console.WriteLine(logicLayer.GetPackage(id).ToString());
		}

		/// <summary>
		/// Display all base stations
		/// </summary>
		/// <param name="logicLayer">The instance of the logic layer</param>
		internal static void DisplayBaseStationList(IBL.IBL logicLayer)
		{
			IEnumerable<IBL.BO.BaseStationListing> stationList = logicLayer.GetBaseStationList();

			foreach (var station in stationList)
			{
				Console.WriteLine(station.ToString() + "\n");
			}
		}

		/// <summary>
		/// Display all drones
		/// </summary>
		/// <param name="logicLayer">The instance of the logic layer</param>
		internal static void DisplayDroneList(IBL.IBL logicLayer)
		{
			IEnumerable<IBL.BO.DroneListing> droneList = logicLayer.GetDroneList();

			foreach (var drone in droneList)
			{
				Console.WriteLine(drone.ToString() + "\n");
			}
		}

		/// <summary>
		/// Display all customers
		/// </summary>
		/// <param name="logicLayer">The instance of the logic layer</param>
		internal static void DisplayCustomerList(IBL.IBL logicLayer)
		{
			IEnumerable<IBL.BO.CustomerListing> customerList = logicLayer.GetCustomerList();

			foreach (var customer in customerList)
			{
				Console.WriteLine(customer.ToString() + "\n");
			}
		}

		/// <summary>
		/// Display all packages
		/// </summary>
		/// <param name="logicLayer">The instance of the logic layer</param>
		internal static void DisplayPackageList(IBL.IBL logicLayer)
		{
			IEnumerable<IBL.BO.PackageListing> packageList = logicLayer.GetPackageList();

			foreach (var package in packageList)
			{
				Console.WriteLine(package.ToString() + "\n");
			}
		}

		/// <summary>
		/// Display all packages that are not assigned
		/// </summary>
		/// <param name="logicLayer">The instance of the logic layer</param>
		internal static void DisplayUnassignedPackages(IBL.IBL logicLayer)
		{
			IEnumerable<IBL.BO.PackageListing> packageList = logicLayer.GetPackageList(x => x.Status == IBL.BO.PackageStatus.created);

			foreach (var package in packageList)
			{
				Console.WriteLine(package.ToString() + "\n");
			}
		}

		/// <summary>
		/// Display all base stations with available charging slots
		/// </summary>
		/// <param name="logicLayer">The instance of the logic layer</param>
		internal static void DisplayAvailableBaseStations(IBL.IBL logicLayer)
		{
			IEnumerable<IBL.BO.BaseStationListing> baseStationList = logicLayer.GetBaseStationList();

			foreach (var station in baseStationList)
			{
				if (station.AvailableChargingSlotsCount > 0)
				{
					Console.WriteLine(station.ToString() + "\n");
				}
			}
		}
	}
}
