using System;
using System.Linq;
using BL;
using DO;
using DalApi;

namespace BlApi
{
    public partial class BL : IBL
    {
        public void ReleaseDroneFromCharge(int id)
        {
            var droneIndex = drones.FindIndex(d => d.Id == id);
            if (droneIndex == -1)
            { throw new DroneNotFoundException(id); }

            var drone = drones[droneIndex];

            DroneCharge droneCharge;
            try { droneCharge = dal.GetDroneCharge(id); }
            catch (IdNotFoundException)
            { throw new DroneNotChargingException(); }

            var station = dal.GetStation(droneCharge.StationId);

            var chargingTime = DateTime.UtcNow - droneCharge.StartTime;

            drone.BatteryStatus = Math.Min(100, drone.BatteryStatus + powerConsumption.ChargeRate * chargingTime.Seconds);
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
                throw new DroneNotFoundException(id);
            }

            var drone = drones[droneIndex];
            if (drone.Status != DroneStatus.free)
            {
                throw new DroneNotFreeException("send drone to charge");
            }

            var maxDistance = drone.BatteryStatus / powerConsumption.Free;
            var reachableStations = dal.GetStationList().Where(s => Utils.DistanceBetween(drone.Location, new(s.Latitude, s.Longitude)) < maxDistance && s.ChargeSlots > 0);
            
            if (reachableStations.Any())
            { throw new NoStationInRangeException(maxDistance); }

            var closestStation = Utils.ClosestStation(drone.Location, reachableStations);

            var batteryUsed = powerConsumption.Free * Utils.DistanceBetween(drone.Location, new(closestStation.Latitude, closestStation.Longitude));

            drone.BatteryStatus -= batteryUsed;
            drone.Status = DroneStatus.maintenance;
            drone.Location = new(closestStation.Latitude, closestStation.Longitude);
            drones[droneIndex] = drone;

            dal.UpdateDrone(id, battery: drone.BatteryStatus);
            dal.UpdateStation(closestStation.Id, chargeSlots: closestStation.ChargeSlots - 1);
            dal.AddDroneCharge(closestStation.Id, id);
        }
    }
}
