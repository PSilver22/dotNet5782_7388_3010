using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public interface IStationEditor
    {
        /// <summary>
        /// Wrapper for bl UpdateBaseStation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="numChargingSlots"></param>
        public void UpdateStation(int id, string name, int numChargingSlots);

        /// <summary>
        /// Gets the list of drones charging at given station
        /// </summary>
        /// <param name="stationId">ID of station to retrieve list of charging drones from</param>
        /// <returns>List of instances of charging drones</returns>
        public List<BL.ChargingDrone> GetChargingDrones(int stationId);
    }
}
