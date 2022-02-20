#nullable enable
using System;
using System.ComponentModel;
using System.Threading;
using static BlApi.BL;
using System.Linq;
using System.Windows;
using BL;


namespace BlApi
{
    public class Simulator
    {
        private readonly IBL _bl;
        private readonly int _droneId;

        private const double DroneSpeed = 20;
        private const int StepTime = 500;
        private const int EarthRadius = 6378137;
        const double threshold = .001;

        private readonly BackgroundWorker _worker = new();

        public Simulator(IBL bl, int droneId, Func<bool> condition, Action updateAction)
        {
            this._bl = bl;
            this._droneId = droneId;

            _worker.DoWork += (_, _) => Run();
            _worker.RunWorkerCompleted += (_, _) =>
            {
                updateAction();
                if (condition())
                    _worker.RunWorkerAsync();
            };
            _worker.RunWorkerAsync();
        }

        private void UpdateChargingDroneBattery()
        {
            Drone drone = _bl.GetDrone(_droneId);

            if (drone.Status == DroneStatus.maintenance)
            {
                lock (_bl.Dal)
                {
                    _bl.Dal.UpdateDrone(_droneId,
                        battery: Math.Min(100, drone.BatteryStatus + _bl.PowerConsumption.ChargeRate));
                }
            }
        }

        private Location NextMove(Location destination)
        {
            Drone drone = _bl.GetDrone(_droneId);

            double totalDLat = Utils.DistanceBetween(drone.Location,
                new Location(destination.Latitude, drone.Location.Longitude));
            double totalDLon = Utils.DistanceBetween(drone.Location,
                new Location(drone.Location.Latitude, destination.Longitude));
            double latLonRation = totalDLat / totalDLon;

            Location direction = GetDirection(drone.Location, destination);

            double dLat = direction.Latitude * (DroneSpeed / (totalDLat + totalDLon)) * totalDLat;
            double dLon = direction.Longitude * (DroneSpeed / (totalDLat + totalDLon)) * totalDLon;

            return new Location(drone.Location.Latitude + ((180 / Math.PI) * (dLat / EarthRadius)),
                drone.Location.Longitude + ((180 / Math.PI) * (dLon / EarthRadius) / Math.Cos(destination.Latitude)));
        }

        private bool InRange(Location destination)
        {
            Drone drone = _bl.GetDrone(_droneId);
            return GetDirection(drone.Location, destination) !=
                   GetDirection(NextMove(destination), destination);
        }

        private Location GetDirection(Location source, Location destination)
        {
            return new Location(
                (destination.Latitude >= source.Latitude) ? 1 : -1,
                (destination.Longitude >= source.Longitude) ? 1 : -1);
        }

        private void MoveNextStep(Location destination)
        {
            Drone drone = _bl.GetDrone(_droneId);
            var nextMove = InRange(destination) ? destination : NextMove(destination);
            
            double distance = Utils.DistanceBetween(drone.Location, nextMove);

            var powerConsump = drone.Package is null
                ? _bl.PowerConsumption.Free
                : _bl.GetPowerConsumption(drone.Package.Weight);

            lock (_bl.Dal)
            {
                _bl.Dal.UpdateDrone(_droneId,
                    longitude: nextMove.Longitude,
                    latitude: nextMove.Latitude,
                    battery: Math.Max(0, drone.BatteryStatus - distance * powerConsump));
            }
        }

        private void Run()
        {
            Thread.Sleep(StepTime);

            lock (_bl)
            {
                var drone = _bl.GetDrone(_droneId);

                switch (drone.Status)
                {
                    case DroneStatus.free:
                        try
                        {
                            _bl.AssignPackageToDrone(drone.Id);
                        }
                        catch (NoRelevantPackageException ex)
                        {
                            if (drone.BatteryStatus < 100)
                            {
                                BaseStation closestStation =
                                    _bl.GetBaseStation(Utils.ClosestStation(drone.Location, _bl.Dal.GetStationList())
                                        .Id);

                                if (drone.Location != closestStation.Location)
                                {
                                    MoveNextStep(closestStation.Location);
                                }
                                else if (closestStation.AvailableChargingSlots > 0)
                                {
                                    _bl.SendDroneToCharge(drone.Id);
                                }
                            }
                        }

                        break;
                    case DroneStatus.maintenance when drone.BatteryStatus < 100:
                        UpdateChargingDroneBattery();
                        break;
                    case DroneStatus.maintenance:
                        _bl.ReleaseDroneFromCharge(_droneId);
                        break;
                    case DroneStatus.delivering:
                    default:
                    {
                        Package package = _bl.GetPackage(drone.Package!.Id);
                        if (package.CollectionTime is null)
                        {
                            if (drone.Location == drone.Package.CollectionLoc)
                            {
                                _bl.CollectPackageByDrone(_droneId);
                            }

                            MoveNextStep(drone.Package.CollectionLoc);
                        }
                        else
                        {
                            if (drone.Location == drone.Package.DeliveryLoc)
                            {
                                _bl.DeliverPackageByDrone(_droneId);
                            }

                            MoveNextStep(drone.Package.DeliveryLoc);
                        }

                        break;
                    }
                }
            }
        }
    }
}