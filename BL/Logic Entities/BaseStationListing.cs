using System;
namespace IBL.BO
{
    public class BaseStationListing
    {
        int Id { get; init; }
        string Name { get; init; }
        int AvailableChargingSlotsCount { get; init; }
        int OccupiedChargingSlotsCount { get; init; }

        public BaseStationListing(int id, string name, int availableChargingSlotsCount, int occupiedChargingSlotsCount)
        {
            Id = id;
            Name = name;
            AvailableChargingSlotsCount = availableChargingSlotsCount;
            OccupiedChargingSlotsCount = occupiedChargingSlotsCount;
        }
    }
}
