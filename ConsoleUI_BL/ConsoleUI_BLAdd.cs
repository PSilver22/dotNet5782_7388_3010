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
		/// Input the data for a base station to be added to the data layer
		/// </summary>
		/// <param name="logicLayer">Instance of the logic layer</param>
		internal static void AddBaseStation(IBL.IBL logicLayer)
		{
			Console.WriteLine("Enter station ID, name, longitude, latitude, and number of available charging stations: ");
			
			int id = Console.Read();
			string name = Console.ReadLine();
			double longitude = Convert.ToDouble(Console.ReadLine());
			double latitude = Convert.ToDouble(Console.ReadLine());
			int numChargingSlots = Console.Read();

			logicLayer.AddBaseStation(id, name, latitude, longitude, numChargingSlots);
		}

		/// <summary>
		/// Input the data for a drone to be added to the data layer
		/// </summary>
		/// <param name="logicLayer">Instance of the logic layer</param>
		internal static void AddDrone(IBL.IBL logicLayer)
		{
			Console.WriteLine("Enter manufacture ID, drone model, max weight <sub menu>, starting station ID: ");
			int id = Console.Read();
			string model = Console.ReadLine();
			IDAL.DO.WeightCategory maxWeight = (IDAL.DO.WeightCategory) Console.Read();
			int startingStationId = Console.Read();

			logicLayer.AddDrone(id, model, maxWeight, startingStationId);
		}

		/// <summary>
		/// Input the data for a customer to be added to the data layer
		/// </summary>
		/// <param name="logicLayer">Instance of the logic layer</param>
		internal static void AddCustomer(IBL.IBL logicLayer)
		{
			Console.WriteLine("Enter customer ID, name, phone, longitude, and latitude: ");
			int id = Console.Read();
			string name = Console.ReadLine();
			string phone = Console.ReadLine();
			double longitude = Convert.ToDouble(Console.ReadLine());
			double latitude = Convert.ToDouble(Console.ReadLine());

			logicLayer.AddCustomer(id, name, phone, longitude, latitude);
		}

		/// <summary>
		/// Input the data for a package to be added to the data layer
		/// </summary>
		/// <param name="logicLayer">Instance of the logic layer</param>
		internal static void AddPackage(IBL.IBL logicLayer)
		{
			Console.WriteLine("Enter sending customer ID, receiving customer ID, weight <sub menu>, and priority <sub menu>: ");
			int senderId = Console.Read();
			int receiverId = Console.Read();
			IDAL.DO.WeightCategory weight = (IDAL.DO.WeightCategory) Console.Read();
			IDAL.DO.Priority priority = (IDAL.DO.Priority) Console.Read();

			logicLayer.AddPackage(senderId, receiverId, weight, priority);
		}
	}
}
