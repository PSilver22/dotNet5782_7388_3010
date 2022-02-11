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
            lock (Dal) {
                var drone = GetDrone(id);

                DroneCharge droneCharge;
                try {
                    droneCharge = Dal.GetDroneCharge(id);
                } catch (IdNotFoundException) {
                    throw new DroneNotChargingException();
                }

                var station = Dal.GetStation(droneCharge.StationId);

                var chargingTime = DateTime.UtcNow - droneCharge.StartTime;

                Dal.UpdateStation(station.Id, chargeSlots: station.ChargeSlots + 1);
                Dal.UpdateDrone(id,
                    battery: Math.Min(100, drone.BatteryStatus + PowerConsumption.ChargeRate * chargingTime.TotalSeconds));
                Dal.DeleteDroneCharge(drone.Id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendDroneToCharge(int id)
        {
            lock (Dal) {
                var drone = GetDrone(id);
                if (drone.Status != DroneStatus.free) {
                    throw new DroneNotFreeException("send drone to charge");
                }

                var maxDistance = drone.BatteryStatus / PowerConsumption.Free;
                var reachableStations = Dal.GetStationList().Where(s =>
                        Utils.DistanceBetween(drone.Location, new(s.Latitude, s.Longitude)) < maxDistance &&
                        s.ChargeSlots > 0)
                    .ToList();

                if (!reachableStations.Any()) {
                    throw new NoStationInRangeException(maxDistance);
                }

                var closestStation = Utils.ClosestStation(drone.Location, reachableStations);

                var batteryUsed = PowerConsumption.Free *
                                  Utils.DistanceBetween(drone.Location,
                                      new(closestStation.Latitude, closestStation.Longitude));

                Dal.UpdateDrone(id,
                    battery: drone.BatteryStatus - batteryUsed,
                    longitude: closestStation.Longitude,
                    latitude: closestStation.Latitude);
                Dal.UpdateStation(closestStation.Id, chargeSlots: closestStation.ChargeSlots - 1);
                Dal.AddDroneCharge(closestStation.Id, id);
            }
        }
    }
}