using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public partial class DroneListWindow : DroneEditorDelegate
    {
        public void AssignPackageToDrone(int id)
        {
            bl.AssignPackageToDrone(id);
            ReloadDrones();
        }

        public void CollectPackageByDrone(int id)
        {
            bl.CollectPackageByDrone(id);
            ReloadDrones();
        }

        public void DeliverPackageByDrone(int id)
        {
            bl.DeliverPackageByDrone(id);
            ReloadDrones();
        }

        public Package GetPackage(int id)
        {
            return bl.GetPackage(id);
        }

        public void ReleaseDroneFromCharge(int id, int chargingTime)
        {
            bl.ReleaseDroneFromCharge(id, chargingTime);
            ReloadDrones();
        }

        public void SendDroneToCharge(int id)
        {
            bl.SendDroneToCharge(id);
            ReloadDrones();
        }

        public void UpdateDrone(int id, string model)
        {
            bl.UpdateDrone(id, model);
            ReloadDrones();
        }
    }
}
