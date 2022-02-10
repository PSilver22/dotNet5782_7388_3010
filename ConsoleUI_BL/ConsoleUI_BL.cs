using System;

namespace ConsoleUI_BL
{
	partial class ConsoleUI
	{
		static void Main(string[] args)
		{
			BlApi.IBL logicLayer = BlApi.BLFactory.GetBl();

			logicLayer.StartSimulator();
			//bool running = true;
			//BlApi.IBL logicLayer = BlApi.BLFactory.GetBl();

			//while (running)
			//{
			//	Utils.DisplayMainMenu();
			//	int option = Utils.ReadInt();

			//	try
			//	{
			//		switch (option)
			//		{
			//			case 1:
			//				Utils.DisplayAddMenu();
			//				option = Utils.ReadInt();

			//				switch (option)
			//				{
			//					case 1:
			//						// add base station
			//						AddBaseStation(logicLayer);
			//						break;
			//					case 2:
			//						// add drone
			//						AddDrone(logicLayer);
			//						break;
			//					case 3:
			//						// add new customer
			//						AddCustomer(logicLayer);
			//						break;
			//					case 4:
			//						// add new package
			//						AddPackage(logicLayer);
			//						break;

			//				}
			//				break;

			//			case 2:
			//				Utils.DisplayUpdateMenu();
			//				option = Utils.ReadInt();

			//				switch (option)
			//				{
			//					case 1:
			//						// update drone name
			//						UpdateDrone(logicLayer);
			//						break;
			//					case 2:
			//						// update station
			//						UpdateStation(logicLayer);
			//						break;
			//					case 3:
			//						// update customer
			//						UpdateCustomer(logicLayer);
			//						break;
			//					case 4:
			//						// Send drone to charge
			//						ChargeDrone(logicLayer);
			//						break;
			//					case 5:
			//						// Release drone from charge
			//						ReleaseDroneCharge(logicLayer);
			//						break;
			//					case 6:
			//						// assign package to drone
			//						AssignDronePackage(logicLayer);
			//						break;
			//					case 7:
			//						// collect a package by drone
			//						CollectPackage(logicLayer);
			//						break;
			//					case 8:
			//						// deliver a package from drone
			//						DeliverPackage(logicLayer);
			//						break;
			//				}
			//				break;

			//			case 3:
			//				Utils.DisplayDisplayMenu();
			//				option = Utils.ReadInt();

			//				switch (option)
			//				{
			//					case 1:
			//						// display base station
			//						DisplayBaseStation(logicLayer);
			//						break;
			//					case 2:
			//						// display drone
			//						DisplayDrone(logicLayer);
			//						break;
			//					case 3:
			//						// display customer
			//						DisplayCustomer(logicLayer);
			//						break;
			//					case 4:
			//						// display package
			//						DisplayPackage(logicLayer);
			//						break;
			//				}

			//				break;

			//			case 4:
			//				Utils.DisplayListMenu();
			//				option = Utils.ReadInt();

			//				switch (option)
			//				{
			//					case 1:
			//						// base stations list
			//						DisplayBaseStationList(logicLayer);
			//						break;
			//					case 2:
			//						// drones list
			//						DisplayDroneList(logicLayer);
			//						break;
			//					case 3:
			//						// customers list
			//						DisplayCustomerList(logicLayer);
			//						break;
			//					case 4:
			//						// packages list
			//						DisplayPackageList(logicLayer);
			//						break;
			//					case 5:
			//						// unassigned packages
			//						DisplayUnassignedPackages(logicLayer);
			//						break;
			//					case 6:
			//						// available base stations
			//						DisplayAvailableBaseStations(logicLayer);
			//						break;
			//				}
			//				break;

			//			case 5:
			//				running = false;
			//				break;
			//		}
			//	}

			//	catch (Exception error)
			//	{
			//		Console.WriteLine(error.Message + "\n");
			//	}
			//}
		}
	}
}