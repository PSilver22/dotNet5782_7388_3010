using System;
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

            DroneCharge droneCharge;
            try { droneCharge = dal.GetDroneCharge(id); }
            catch
            { /* TODO: throw DroneNotCharging */ throw new Exception(); }

            var station = dal.GetStation(droneCharge.StationId);

            drone.BatteryStatus = Math.Min(100, drone.BatteryStatus + powerConsumption.ChargeRate * chargingTime);
            drone.Status = DroneStatus.free;
            drones[droneIndex] = drone;

            dal.UpdateStation(station.Id, chargeSlots: station.ChargeSlots + 1);
            dal.UpdateDrone(drone.Id, battery: drone.BatteryStatus);
            dal.DeleteDroneCharge(drone.Id);
        }

        public void SendDroneToCharge(int id)
        {
            var droneIndex = drones.FindIndex(d => d.Id == id);
            if (droneIndex == -1)
            {
                // TODO: throw DroneNotFound
            }

            var drone = drones[droneIndex];
            if (drone.Status != DroneStatus.free)
            {
                // TODO: throw DroneNotFree
            }

            var maxDistance = drone.BatteryStatus / powerConsumption.Free;
            var reachableStations = dal.GetStationList().FindAll(s => Utils.DistanceBetween(drone.Location, new(s.Latitude, s.Longitude)) < maxDistance && s.ChargeSlots > 0);
            if (reachableStations.Count == 0)
            { /* TODO: throw NoStationInRange */ }

            var closestStation = Utils.ClosestStation(drone.Location, reachableStations);

            var batteryUsed = powerConsumption.Free * Utils.DistanceBetween(drone.Location, new(closestStation.Latitude, closestStation.Longitude));

            drone.BatteryStatus -= batteryUsed;
            drone.Status = DroneStatus.maintenance;
            drone.Location = new(closestStation.Latitude, closestStation.Longitude);
            drones[droneIndex] = drone;

            dal.UpdateDrone(id, battery: drone.BatteryStatus);
            dal.UpdateStation(closestStation.Id, chargeSlots: closestStation.ChargeSlots - 1);
            dal.AddDroneCharge(id, closestStation.Id);
        }
    }
}
