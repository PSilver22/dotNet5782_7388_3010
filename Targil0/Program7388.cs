using System;

namespace Targil0
{
	partial class Program
	{
		static void Main(string[] args)
		{
			Welcome7388();
			Welcome3010();
			Console.ReadKey();
		}

		static partial void Welcome3010();

		private static void Welcome7388()
		{
			// get the user's name
			Console.Write("Enter your name: ");
			string name = Console.ReadLine();

			// print welcome message
			Console.WriteLine($"{name}, welcome to my first console application.");
		}
	}
}
