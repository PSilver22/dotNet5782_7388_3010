using System;
using System.Collections.Generic;
using IBL.BO;
using IDAL.DO;

namespace IBL
{
    public partial class BL : IBL
    {
        public void ReleaseDroneFromCharge(int id, int chargingTime)
        {
            var droneIndex = drones.FindIndex(d => d.Id == id);
            if (droneIndex == -1)
            {
                // TODO: throw DroneNotFound
            }


            var drone = drones[droneIndex];
            if (drone.Status != DroneStatus.maintenance)
            {
                // TODO: throw DroneNotInMaintenance
            }

            // TODO: move logic to BL
            try { dal.ReleaseDrone(id); }
            catch
            { /* TODO: throw DroneNotFound */ }

            drone.BatteryStatus = Math.Min(100, drone.BatteryStatus + powerConsumption.ChargeRate * chargingTime);
            drones[droneIndex] = drone;
        }

        public void SendDroneToCharge(int id)
        {
            throw new NotImplementedException();
        }
    }
}
