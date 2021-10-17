using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
	namespace DO
	{
		/// <summary>
		/// Structure containing package information
		/// </summary>
		public struct Package : IIdentifiable
		{
			public int Id { get; set; }
			public int SenderId { get; set; }
			public int TargetId { get; set; }
			public WeightCategory Weight { get; set; }
			public Priority priority { get; set; }
			public DateTime Requested { get; set; }
			public int DroneId { get; set; }
			public DateTime Scheduled { get; set; }
			public DateTime PickedUp { get; set; }
			public DateTime Delivered { get; set; }

			/// <summary>
			/// Creates a string with the package information
			/// </summary>
			/// <returns>
			/// String with the package information
			/// </returns>
			public override string ToString()
			{
				return
					$"Parcel: {Id}\n" +
					$"Sender ID: {SenderId}\n" +
					$"Target ID: {TargetId}\n" +
					$"Weight class: {Weight.ToString()}\n" +
					$"Priority: {priority.ToString()}\n" +
					$"Date requested: {Requested.ToLongDateString()}\n" +
					$"Drone ID: {DroneId}\n" +
					$"Scheduled date: {Scheduled.ToLongDateString()}\n" +
					$"Pick up date: {PickedUp.ToLongDateString()}\n" +
					$"Delivered date: {Delivered.ToLongDateString()}\n";
			}
		}
	}
}
