using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace ConsoleUI_BL
{
	class Utils
	{
		internal static Priority PromptPriority()
		{
			Console.WriteLine("Enter a priority: <0: regular, 1: fast, 2: emergency>");
			return (Priority) ReadInt();
		}

		internal static WeightCategory PromptWeightCategory()
		{
			Console.WriteLine("Enter a weight: <0: light, 1: medium, 2: heavy>");
			return (WeightCategory) ReadInt();
		}

		internal static int ReadInt()
		{
			string input = Console.ReadLine();
			int result;

			if (!int.TryParse(input, out result))
			{
				result = -1;
			}

			return result;
		}


		internal static void DisplayMainMenu()
		{
			Console.WriteLine(
				"Options:\n" +
				"1. Add to the data\n" +
				"2. Update the data\n" +
				"3. Display data\n" +
				"4. Display lists\n" +
				"5. Exit");
		}

		internal static void DisplayAddMenu()
		{
			Console.WriteLine(
				"Options:\n" +
				"1. Base Station\n" +
				"2. Drone\n" +
				"3. New Customer\n" +
				"4. Package");
		}

		internal static void DisplayUpdateMenu()
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
				"8. deliver package from drone");
		}

		internal static void DisplayDisplayMenu()
		{
			Console.WriteLine(
				"Options:\n" +
				"1. Display base station\n" +
				"2. Display drone\n" +
				"3. Display customer\n" +
				"4. display package");
		}

		internal static void DisplayListMenu()
		{
			Console.WriteLine(
				"Options:\n" +
				"1. Base stations list\n" +
				"2. Drones list\n" +
				"3. Customers list\n" +
				"4. Packages list\n" +
				"5. Unassigned packages\n" +
				"6. Available base stations");
		}
	}
}
