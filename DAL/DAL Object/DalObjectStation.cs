#nullable enable

using System;
using System.Linq;
using System.Collections.Generic;
using DO;
using DalApi;

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

            if (DataSource.drones.Exists(s => s.Id == station.Id))
            {
                throw new DuplicatedIdException(station.Id, "station");
            }

            DataSource.stations.Add(station);
        }

        /// <summary>
        /// Returns the station list with information for every station in the stations list
        /// </summary>
        /// <param name="filter">The filter applied to the objects in the list</param>
        /// <returns>
        /// Station list
        /// </returns>
        public IEnumerable<Station> GetStationList(Predicate<Station>? filter = null)
        {
            return DataSource.stations.Where(new Func<Station, bool>(filter ?? (x => true))).ToList();
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
        /// Changes the data of a station in the database
        /// </summary>
        /// <param name="id"> ID of the station to change. </param>
        /// <param name="name"> The new name of the station. </param>
        /// <param name="longitude"> The new longitude of the station. </param>
        /// <param name="latitude"> The new latitude of the station. </param>
        /// <param name="chargeSlot"> The updated amount of charge slots. </param>
        public void UpdateStation(int id, string? name = null, double? longitude = null, double? latitude = null, int? chargeSlot = null)
        {
            int index = GetStationIndex(id);

            Station updatedStation = DataSource.stations[index];

            updatedStation.Name = name ?? updatedStation.Name;
            updatedStation.Longitude = longitude ?? updatedStation.Longitude;
            updatedStation.Latitude = latitude ?? updatedStation.Latitude;
            updatedStation.ChargeSlots = chargeSlot ?? updatedStation.ChargeSlots;

            SetStation(updatedStation);
        }

        /// <summary>
        /// Sets the station with the matching id to the given station
        /// </summary>
        /// <param name="station"></param>
        public void SetStation(Station station)
        {
            int index = GetStationIndex(station.Id);

            DataSource.stations[index] = station;
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
