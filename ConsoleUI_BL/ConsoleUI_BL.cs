using System;

namespace ConsoleUI_BL
{
	class ConsoleUI
	{
		static void Main(string[] args)
		{
			bool running = true;
			while (running)
			{
				displayMainMenu();
				int option = Console.Read();

				try
				{
					switch (option)
					{
						case 1:
							displayAddMenu();
							option = Console.Read();

							switch (option)
							{
								case 1:
									// add base station
									Console.WriteLine("Input station ID, name, location, and number of charging stations: ");
									
									break;
								case 2:
									// add drone
									break;
								case 3:
									// add new customer
									break;
								case 4:
									// add new package
									break;

							}
							break;

						case 2:
							displayUpdateMenu();
							option = Console.Read();

							switch (option)
							{
								case 1:
									// update drone name
									break;
								case 2:
									// update station
									break;
								case 3:
									// update customer
									break;
								case 4:
									// Send drone to charge
									break;
								case 5:
									// Release drone from charge
									break;
								case 6:
									// assign package to drone
									break;
								case 7:
									// collect a package by drone
									break;
								case 8:
									// deliver a package from drone
									break;
							}
							break;

						case 3:
							displayDisplayMenu();
							option = Console.Read();

							switch (option)
							{
								case 1:
									// display base station
									break;
								case 2:
									// display drone
									break;
								case 3:
									// display customer
									break;
								case 4:
									// display package
									break;
								case 5:
									// display base station
									break;
							}

							break;

						case 4:
							displayListMenu();
							option = Console.Read();

							switch (option)
							{
								case 1:
									// base stations list
									break;
								case 2:
									// drones list
									break;
								case 3:
									// customers list
									break;
								case 4:
									// packages list
									break;
								case 5:
									// unassigned packages
									break;
								case 6:
									// available base stations
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

		static void displayMainMenu()
		{
			Console.WriteLine(
				"Options:\n" +
				"1. Add to the data\n" +
				"2. Update the data\n" +
				"3. Display data\n" +
				"4. Display lists\n" +
				"5. Exit\n");
		}

		static void displayAddMenu()
		{
			Console.WriteLine(
				"Options:\n" +
				"1. Base Station\n" +
				"2. Drone" +
				"3. New Customer" +
				"4. Package\n");
		}

		static void displayUpdateMenu()
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

		static void displayDisplayMenu()
		{
			Console.WriteLine(
				"Options:\n" +
				"1. Display base station\n" +
				"2. Display drone\n" +
				"3. Display customer\n" +
				"4. display package\n");
		}

		static void displayListMenu()
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