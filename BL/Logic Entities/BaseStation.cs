using System;
using System.Collections.Generic;

namespace IBL.BO
{
    public class BaseStation
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public Location Location { get; init; }
        public int AvailableChargingSlots { get; init; }
        public List<ChargingDrone> ChargingDrones { get; init; }

        public BaseStation(int id, string name, Location location, int availableChargingSlots, List<ChargingDrone> chargingDrones)
        {
            Id = id;
            Name = name;
            Location = location;
            AvailableChargingSlots = availableChargingSlots;
            ChargingDrones = chargingDrones;
        }

        public override string ToString()
        {
            return $"id: {Id}\n" +
                $"name: {Name}\n" +
                $"location: {Location}\n" +
                $"available charge slots: {AvailableChargingSlots}\n" +
                "charging drones:\n" + String.Join("\n", ChargingDrones);
        }
    }
}
