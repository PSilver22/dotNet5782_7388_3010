namespace PL
{
    public partial class StationListWindow : Utilities.IObserver<UpdateStationPage>
    {
        /// <summary>
        /// IObserver update method
        /// </summary>
        /// <param name="updateStation">The station that was updated</param>
        public void Update(UpdateStationPage updateStation) {
            ReloadStations();
        }
    }
}
