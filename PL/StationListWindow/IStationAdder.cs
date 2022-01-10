using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public interface IStationAdder
    {
        /// <summary>
        /// Wrapper for bl AddStation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="numChargingSlots"></param>
        public void AddStation(int id, string name, double latitude, double longitude, int numChargingSlots);
    }
}
