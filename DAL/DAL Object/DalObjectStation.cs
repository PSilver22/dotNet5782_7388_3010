using System;
using System.Linq;
using System.Collections.Generic;
using IDAL.DO;

namespace DalObject
{
	public partial class DalObject
	{
        /// <summary>
        /// Adds a station to the list of base stations
        /// </summary>
        /// <param name="station">The station to add</param>
        /// <returns>true if the station was successfully added, false otherwise</returns>
        public void AddStation(Station station)
        {
            if (station.Id < 0)
            {
                throw new InvalidIdException(station.Id);
            }

            if (DataSource.stations.Count < DataSource.MaxStations)
            {
                DataSource.stations.Add(station);
            }

            throw new MaximumCapacityException("Station list is at max capacity.");
        }

        /// <summary>
        /// Creates a string with the information for every station in the stations list
        /// </summary>
        /// <returns>
        /// string with the information for every station
        /// </returns>
        public string GetStationList()
        {
            return ListItems<Station>(DataSource.stations);
        }

        /// <summary>
        /// gets a station by id from the stations array
        /// </summary>
        /// <param name="id">id of the station</param>
        /// <returns>Station with the given id if found. throws otherwise</returns>
        public Station GetStation(int id)
        {
            return GetItemByKey<Station>(id, DataSource.stations);
        }

        /// <summary>
        /// Creates a string with the information for every unoccupied station in the stations list
        /// </summary>
        /// <returns>
        /// string with the information for every unoccupied station
        /// </returns>
        public string GetUnoccupiedStationsList()
        {
            return string.Join(
                "\n",
                DataSource.stations.GetRange(0, DataSource.stations.Count)
                    .Where(s => s.ChargeSlots > 0));
        }

        /// <summary>
        /// gets a station's index by id from the stations array
        /// </summary>
        /// <param name="id">id of the station</param>
        /// <returns>Index of the station with the given id if found.</returns>
        private int GetStationIndex(int id)
        {
            return GetItemIndexByKey<Station>(id, DataSource.stations);
        }
    }
}
