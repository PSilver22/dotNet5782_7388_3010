using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public partial class DroneListWindow : DroneAdderDelegate
    {
        public void AddDrone(int id, string model, WeightCategory maxWeight, int startingStationId)
        {
            bl.AddDrone(id, model, maxWeight, startingStationId);
            ReloadDrones();
        }

        public List<BaseStationListing> GetBaseStationList()
        {
            return bl.GetBaseStationList();
        }
    }
}
