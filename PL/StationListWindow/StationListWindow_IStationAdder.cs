namespace PL
{
    public partial class StationListWindow : IStationAdder
    {
        /// <summary>
        /// bl.AddBaseStation wrapper method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="numChargingSlots"></param>
        public void AddStation(int id, string name, double latitude, double longitude, int numChargingSlots) {
            bl.AddBaseStation(id, name, latitude, longitude, numChargingSlots);
            ReloadStations();
        }
    }
}