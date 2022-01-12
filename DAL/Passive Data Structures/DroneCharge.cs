using System;

namespace DO
{
    /// <summary>
    /// A struct with the charging information between a drone and a station
    /// </summary>
    public struct DroneCharge
    {
        public int StationId { get; set; }
        public int DroneId { get; set; }
        public DateTime StartTime { get; set; }

        public DroneCharge(int stationId, int droneId, DateTime startTime)
        {
            StationId = stationId;
            DroneId = droneId;
            StartTime = startTime;
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
