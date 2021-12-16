using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBL.BO;

namespace PL
{
    public interface IDroneEditor
    {
        public void UpdateDrone(int id, string model);
        public void ReleaseDroneFromCharge(int id, int chargingTime);
        public void SendDroneToCharge(int id);
        public void AssignPackageToDrone(int id);
        public void CollectPackageByDrone(int id);
        public void DeliverPackageByDrone(int id);
        public Package GetPackage(int id);
    }
}
