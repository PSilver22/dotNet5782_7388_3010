namespace DO
{
    /// <summary>
    /// A struct with the charging information between a drone and a station
    /// </summary>
    public struct DroneCharge
    {
        public int StationId { get; set; }
        public int DroneId { get; set; }

        public DroneCharge(int stationId, int droneId)
        {
            StationId = stationId;
            DroneId = droneId;
        }

        /// <summary>
        /// Creates a string with the charge information
        /// </summary>
        /// <returns>
        /// The string with the charge information
        /// </returns>
        public override string ToString()
        {
            return $"Drone {DroneId} is charging from station {StationId}";
        }
    }
}
