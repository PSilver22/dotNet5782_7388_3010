using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public partial class StationListWindow : IStationEditor
    {
        /// <summary>
        /// bl.UpdateBaseStation wrapper method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="numChargingSlots"></param>
        public void UpdateStation(int id, string name, int numChargingSlots) {
            bl.UpdateBaseStation(id, name, numChargingSlots);
            ReloadStations();
        }

        /// <summary>
        /// Returns a list of drones charging at station with given ID
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public List<BL.ChargingDrone> GetChargingDrones(int stationId) {
            BL.BaseStation station = bl.GetBaseStation(stationId);

            return station.ChargingDrones;
        }
    }
}
