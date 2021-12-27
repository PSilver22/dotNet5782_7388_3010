using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
	public class NoAvailableChargingSlotsException : Exception
	{
		public NoAvailableChargingSlotsException(int stationId) : base($"Station {stationId} has no available charging slots.") { }
	}
}
