﻿using System.Collections.Generic;
using BL;

namespace PL
{
    public interface IDroneAdder
    {
        public void AddDrone(int id, string model, DO.WeightCategory maxWeight, int startingStationId);
        public IEnumerable<BaseStationListing> GetBaseStationList();
    }
}