using System;
using System.Collections.Generic;

namespace IBL.BO
{
    public class BaseStation
    {
        int Id { get; init; }
        string Name { get; init; }
        Location Location { get; init; }
        int AvailableChargingSlots { get; init; }
        List<ChargingDrone> ChargingDrones { get; init; }

        public BaseStation(int id, string name, Location location, int availableChargingSlots, List<ChargingDrone> chargingDrones)
        {
            Id = id;
            Name = name;
            Location = location;
            AvailableChargingSlots = availableChargingSlots;
            ChargingDrones = chargingDrones;
        }
    }
}
