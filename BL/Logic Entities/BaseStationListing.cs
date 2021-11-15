using System;
namespace IBL.BO
{
    public class BaseStationListing
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int AvailableChargingSlotsCount { get; init; }
        public int OccupiedChargingSlotsCount { get; init; }

        public BaseStationListing(int id, string name, int availableChargingSlotsCount, int occupiedChargingSlotsCount)
        {
            Id = id;
            Name = name;
            AvailableChargingSlotsCount = availableChargingSlotsCount;
            OccupiedChargingSlotsCount = occupiedChargingSlotsCount;
        }

        public override string ToString()
        {
            return $"id: {Id}\n" +
                $"name: {Name}\n" +
                $"available charge slots: {AvailableChargingSlotsCount}\n" +
                $"occupied charging slots: {OccupiedChargingSlotsCount}";
        }
    }
}
