using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public partial class DroneListWindow : IDroneAdder
    {
        /// <summary>
        /// Adds a drone to the list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="startingStationId"></param>
        public void AddDrone(int id, string model, WeightCategory maxWeight, int startingStationId)
        {
            bl.AddDrone(id, model, maxWeight, startingStationId);
            ReloadDrones();
        }

        /// <summary>
        /// Gets the base station list from the logic layer
        /// </summary>
        /// <returns> the base station list form the logic layer</returns>
        public List<BaseStationListing> GetBaseStationList()
        {
            return bl.GetBaseStationList();
        }
    }
}
