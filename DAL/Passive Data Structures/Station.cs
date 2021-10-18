﻿namespace IDAL
{
	namespace DO
	{
		/// <summary>
		/// Struct containing the information of a Station
		/// </summary>
		public struct Station : IIdentifiable
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public double Longitude { get; set; }
			public double Latitude { get; set; }

			public int ChargeSlots { get; set; }

			/// <summary>
			/// Creates a string with the station info
			/// </summary>
			/// <returns>
			///	Returns the string with the station info
			/// </returns>
			public override string ToString()
			{
				return
					$"Station: {Id}\n" +
					$"Name: {Name}\n" +
					$"Longitude: {Longitude}\n" +
					$"Latitude: {Latitude}\n" +
					$"Charge slots: {ChargeSlots}\n";
			}
		}
	}
}