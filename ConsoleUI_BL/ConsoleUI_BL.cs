using System;

namespace ConsoleUI_BL
{
	partial class ConsoleUI
	{
		static void Main(string[] args)
		{
			bool running = true;
			IBL.IBL logicLayer = new IBL.BL();

			while (running)
			{
				DisplayMainMenu();
				int option = Console.Read();

				try
				{
					switch (option)
					{
						case 1:
							DisplayAddMenu();
							option = Console.Read();

							switch (option)
							{
								case 1:
									// add base station
									AddBaseStation(logicLayer);
									break;
								case 2:
									// add drone
									AddDrone(logicLayer);
									break;
								case 3:
									// add new customer
									AddCustomer(logicLayer);
									break;
								case 4:
									// add new package
									AddPackage(logicLayer);
									break;

							}
							break;

						case 2:
							DisplayUpdateMenu();
							option = Console.Read();

							switch (option)
							{
								case 1:
									// update drone name
									UpdateDrone(logicLayer);
									break;
								case 2:
									// update station
									UpdateStation(logicLayer);
									break;
								case 3:
									// update customer
									UpdateCustomer(logicLayer);
									break;
								case 4:
									// Send drone to charge
									ChargeDrone(logicLayer);
									break;
								case 5:
									// Release drone from charge
									ReleaseDroneCharge(logicLayer);
									break;
								case 6:
									// assign package to drone
									AssignDronePackage(logicLayer);
									break;
								case 7:
									// collect a package by drone
									CollectPackage(logicLayer);
									break;
								case 8:
									// deliver a package from drone
									DeliverPackage(logicLayer);
									break;
							}
							break;

						case 3:
							DisplayDisplayMenu();
							option = Console.Read();

							switch (option)
							{
								case 1:
									// display base station
									DisplayBaseStation(logicLayer);
									break;
								case 2:
									// display drone
									DisplayDrone(logicLayer);
									break;
								case 3:
									// display customer
									DisplayCustomer(logicLayer);
									break;
								case 4:
									// display package
									DisplayPackage(logicLayer);
									break;
							}

							break;

						case 4:
							DisplayListMenu();
							option = Console.Read();

							switch (option)
							{
								case 1:
									// base stations list
									DisplayBaseStationList(logicLayer);
									break;
								case 2:
									// drones list
									DisplayDroneList(logicLayer);
									break;
								case 3:
									// customers list
									DisplayCustomerList(logicLayer);
									break;
								case 4:
									// packages list
									DisplayPackageList(logicLayer);
									break;
								case 5:
									// unassigned packages
									DisplayUnassignedPackages(logicLayer);
									break;
								case 6:
									// available base stations
									DisplayAvailableBaseStations(logicLayer);
									break;
							}
							break;

						case 5:
							running = false;
							break;
					}
				}

				catch (Exception error)
				{
					Console.WriteLine(error.Message + "\n");
				}
			}
		}

		static void DisplayMainMenu()
		{
			Console.WriteLine(
				"Options:\n" +
				"1. Add to the data\n" +
				"2. Update the data\n" +
				"3. Display data\n" +
				"4. Display lists\n" +
				"5. Exit\n");
		}

		static void DisplayAddMenu()
		{
			Console.WriteLine(
				"Options:\n" +
				"1. Base Station\n" +
				"2. Drone" +
				"3. New Customer" +
				"4. Package\n");
		}

		static void DisplayUpdateMenu()
		{
			Console.WriteLine(
				"Options:\n" +
				"1. Drone name\n" +
				"2. Station\n" +
				"3. Customer\n" +
				"4. Send drone to charge\n" +
				"5. Release drone from charge\n" +
				"6. Assign package to drone\n" +
				"7. Collect package by drone\n" +
				"8. deliver package from drone\n");
		}

		static void DisplayDisplayMenu()
		{
			Console.WriteLine(
				"Options:\n" +
				"1. Display base station\n" +
				"2. Display drone\n" +
				"3. Display customer\n" +
				"4. display package\n");
		}

		static void DisplayListMenu()
		{
			Console.WriteLine(
				"Options:\n" +
				"1. Base stations list\n" +
				"2. Drones list\n" +
				"3. Customers list\n" +
				"4. Packages list\n" +
				"5. Unassigned packages\n" +
				"6. Available base stations\n");
		}
	}
}