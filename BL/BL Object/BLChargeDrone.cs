using System;
using System.Linq;
using BL;
using DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace BlApi
{
    public partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseDroneFromCharge(int id)
        {
            var drone = GetDrone(id);

            DroneCharge droneCharge;
            try
            {
                droneCharge = dal.GetDroneCharge(id);
            }
            catch (IdNotFoundException)
            {
                throw new DroneNotChargingException();
            }

                var station = dal.GetStation(droneCharge.StationId);

                var chargingTime = DateTime.UtcNow - droneCharge.StartTime;

            dal.UpdateStation(station.Id, chargeSlots: station.ChargeSlots + 1);
            dal.UpdateDrone(id,
                battery: Math.Min(100, drone.BatteryStatus + powerConsumption.ChargeRate * chargingTime.TotalSeconds));
            dal.DeleteDroneCharge(drone.Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendDroneToCharge(int id)
        {
            var drone = GetDrone(id);
            if (drone.Status != DroneStatus.free)
            {
                throw new DroneNotFreeException("send drone to charge");
            }

            var maxDistance = drone.BatteryStatus / powerConsumption.Free;
            var reachableStations = dal.GetStationList().Where(s =>
                    Utils.DistanceBetween(drone.Location, new(s.Latitude, s.Longitude)) < maxDistance &&
                    s.ChargeSlots > 0)
                .ToList();

            if (!reachableStations.Any())
            {
                throw new NoStationInRangeException(maxDistance);
            }

                var closestStation = Utils.ClosestStation(drone.Location, reachableStations);

            var batteryUsed = powerConsumption.Free *
                              Utils.DistanceBetween(drone.Location,
                                  new(closestStation.Latitude, closestStation.Longitude));

            dal.UpdateDrone(id,
                battery: drone.BatteryStatus - batteryUsed,
                longitude: closestStation.Longitude,
                latitude: closestStation.Latitude);
            dal.UpdateStation(closestStation.Id, chargeSlots: closestStation.ChargeSlots - 1);
            dal.AddDroneCharge(closestStation.Id, id);
        }
    }
}