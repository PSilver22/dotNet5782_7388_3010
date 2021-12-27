#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
	partial class ConsoleUI
	{
		/// <summary>
		/// Update the data of a drone with the inputted id
		/// </summary>
		/// <param name="logicLayer">Instance of the logic layer</param>
		internal static void UpdateDrone(BlApi.IBL logicLayer)
		{
			Console.WriteLine("Enter the current drone ID: ");
			int oldDroneId = Utils.ReadInt();

			Console.WriteLine("Enter new model: ");
			string newModel = Console.ReadLine() ?? "";

			logicLayer.UpdateDrone(oldDroneId, newModel);
		}

		/// <summary>
		/// Update the data of a station with the inputted id
		/// </summary>
		/// <param name="logicLayer">Instance of the logic layer</param>
		internal static void UpdateStation(BlApi.IBL logicLayer)
		{
			Console.WriteLine("Enter the current station id: ");
			int id = Utils.ReadInt();

			Console.WriteLine("Enter one or more of the following: New station name, additional number of charging stations\n");
			string? newName = Console.ReadLine();
			int additionalChargingStations = Utils.ReadInt();

			logicLayer.UpdateBaseStation(id, newName, (additionalChargingStations == -1) ? null : additionalChargingStations);
		}

		/// <summary>
		/// Update the data of a customer with the inputted id
		/// </summary>
		/// <param name="logicLayer">Instance of the logic layer</param>
		internal static void UpdateCustomer(BlApi.IBL logicLayer)
		{
			Console.WriteLine("Enter the current customer id: ");
			int id = Utils.ReadInt();

			Console.WriteLine("Enter one or more of the following: new name, new phone\n");
			string? newName = Console.ReadLine();
			string? newPhone = Console.ReadLine();

			logicLayer.UpdateCustomer(id, newName, newPhone);
		}

		/// <summary>
		/// Charge the drone with the inputted id
		/// </summary>
		/// <param name="logicLayer">Instance of the logic layer</param>
		internal static void ChargeDrone(BlApi.IBL logicLayer)
		{
			Console.WriteLine("Enter the drone ID: ");
			int id = Utils.ReadInt();

			logicLayer.SendDroneToCharge(id);
		}

		/// <summary>
		/// Release the drone with the inputted id from charging
		/// </summary>
		/// <param name="logicLayer">Instance of the logic layer</param>
		internal static void ReleaseDroneCharge(BlApi.IBL logicLayer)
		{
			Console.WriteLine("Enter the drone ID and charging time: ");
			int id = Utils.ReadInt();
			int time = Utils.ReadInt();

			logicLayer.ReleaseDroneFromCharge(id, time);
		}

		/// <summary>
		/// Assign a package to a drone
		/// </summary>
		/// <param name="logicLayer">Instance of the logic layer</param>
		internal static void AssignDronePackage(BlApi.IBL logicLayer)
		{
			Console.WriteLine("Enter the drone ID: ");
			int id = Utils.ReadInt();

			logicLayer.AssignPackageToDrone(id);
		}

		/// <summary>
		/// Collect package of the drone with the inputted id
		/// </summary>
		/// <param name="logicLayer">Instance of the logic layer</param>
		internal static void CollectPackage(BlApi.IBL logicLayer)
		{
			Console.WriteLine("Enter the drone ID: ");
			int id = Utils.ReadInt();

			logicLayer.CollectPackageByDrone(id);
		}

		/// <summary>
		/// Deliver package from the drone with the inputted id
		/// </summary>
		/// <param name="logicLayer">Instance of the logic layer</param>
		internal static void DeliverPackage(BlApi.IBL logicLayer)
		{
			Console.WriteLine("Enter the drone ID: ");
			int id = Utils.ReadInt();

			logicLayer.DeliverPackageByDrone(id);
		}
	}
}
