using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public partial class DroneListWindow : IDroneEditor
    {
        /// <summary>
        /// Assigns a pakcage to given drone and updates the drone list
        /// </summary>
        /// <param name="id">ID of the drone to assign to</param>
        public void AssignPackageToDrone(int id)
        {
            bl.AssignPackageToDrone(id);
            ReloadDrones();
        }

        /// <summary>
        /// Has drone collect a package then updates the drone list
        /// </summary>
        /// <param name="id"></param>
        public void CollectPackageByDrone(int id)
        {
            bl.CollectPackageByDrone(id);
            ReloadDrones();
        }

        /// <summary>
        /// Delivers package being carried by a drone
        /// </summary>
        /// <param name="id">Drone to deliver package</param>
        public void DeliverPackageByDrone(int id)
        {
            bl.DeliverPackageByDrone(id);
            ReloadDrones();
        }

        /// <summary>
        /// Gets package from the logic layer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>package from logic layers</returns>
        public Package GetPackage(int id)
        {
            return bl.GetPackage(id);
        }

        /// <summary>
        /// Releases drone from charge and updates the drone list
        /// </summary>
        /// <param name="id">ID of the drone to release</param>
        /// <param name="chargingTime">Time to charge in hours</param>
        public void ReleaseDroneFromCharge(int id, int chargingTime)
        {
            bl.ReleaseDroneFromCharge(id, chargingTime);
            ReloadDrones();
        }

        /// <summary>
        /// Sends a drone to charge at the nearest station
        /// </summary>
        /// <param name="id"></param>
        public void SendDroneToCharge(int id)
        {
            bl.SendDroneToCharge(id);
            ReloadDrones();
        }

        /// <summary>
        /// Updates drone model 
        /// </summary>
        /// <param name="id">ID of the drone to update</param>
        /// <param name="model">New model of the drone</param>
        public void UpdateDrone(int id, string model)
        {
            bl.UpdateDrone(id, model);
            ReloadDrones();
        }
    }
}
