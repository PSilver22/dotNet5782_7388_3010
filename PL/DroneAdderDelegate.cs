using System.Collections.Generic;
using IBL.BO;

namespace PL
{
    public interface DroneAdderDelegate
    {
        public void AddDrone(int id, string model, IDAL.DO.WeightCategory maxWeight, int startingStationId);
        public List<BaseStationListing> GetBaseStationList();
    }
}
