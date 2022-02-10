#nullable enable
using System;
using System.Threading;
using static BlApi.BL;
using System.Linq;
using System.Windows;
using BL;


namespace BlApi {
    class Simulator {
        public BL bl;
        Func<bool> condition;
        Action updateAction;
        int droneId;

        const double droneSpeed = .5;
        const int stepTime = 1000;
        const int earthRadius = 6378137;
        const double threshold = .001;

        public Simulator(BL bl, int droneId, Func<bool> condition, Action updateAction) {
            this.bl = bl;
            this.droneId = droneId;
            this.condition = condition;
            this.updateAction = updateAction;

            Run();
        }

        private void SimulatorUpdateDroneBattery(Location? startPos = null, Location? endPos = null) {
            Drone drone = bl.GetDrone(droneId);

            if (drone.Status == DroneStatus.maintenance) {
                lock (bl.dal) {
                    bl.dal.UpdateDrone(droneId,
                        battery: Math.Min(100, drone.BatteryStatus + bl.powerConsumption.ChargeRate));
                }
            } else {
                lock (bl.dal) {
                    double distance = (startPos != null) ? Utils.DistanceBetween(startPos, endPos) : 1;

                    bl.dal.UpdateDrone(droneId,
                        battery: Math.Max(0, drone.BatteryStatus - distance * bl.GetPowerConsumption(drone.WeightCategory)));
                }
            }
        }

        private Location NextMove(Location destination) {
            Drone drone = bl.GetDrone(droneId);

            double totalDLat = Utils.DistanceBetween(drone.Location,
                new Location(destination.Latitude, drone.Location.Longitude));
            double totalDLon = Utils.DistanceBetween(drone.Location,
                new Location(drone.Location.Latitude, destination.Longitude));
            double latLonRation = totalDLat / totalDLon;

            Location direction = GetDirection(drone.Location, destination);

            double dLat = direction.Latitude * (droneSpeed / (totalDLat + totalDLon)) * totalDLat;
            double dLon = direction.Longitude * (droneSpeed / (totalDLat + totalDLon)) * totalDLon;

            return new Location(drone.Location.Latitude + ((180 / Math.PI) * (dLat / earthRadius)),
                drone.Location.Longitude + ((180 / Math.PI) * (dLon / earthRadius) / Math.Cos(destination.Latitude)));
        }

        private bool InRange(Location destination) {
            Drone drone = bl.GetDrone(droneId);
            return GetDirection(drone.Location, destination) !=
                GetDirection(NextMove(destination), destination);
        }

        private Location GetDirection(Location source, Location destination) {
            return new Location(
                (destination.Latitude >= source.Latitude) ? 1 : -1,
                (destination.Longitude >= source.Longitude) ? 1 : -1);
        }

        private void MoveNextStep(Location destination) {
            Drone drone = bl.GetDrone(droneId);
            double distance = Utils.DistanceBetween(destination, drone.Location);
            var nextMove = NextMove(destination);

            lock (bl.dal) {
                if (InRange(destination)) {
                    bl.dal.UpdateDrone(droneId,
                        longitude: destination.Longitude,
                        latitude: destination.Latitude);
                } else {
                    bl.dal.UpdateDrone(droneId,
                        longitude: nextMove.Longitude,
                        latitude: nextMove.Latitude);
                }
            }
        }

        public void Run() {
            while (condition.Invoke()) {
                Thread.Sleep(stepTime);

                lock (bl) {
                    updateAction.Invoke();

                    Drone drone = bl.GetDrone(droneId);

                    if (drone.Status == DroneStatus.free) {
                        try {
                            bl.AssignPackageToDrone(drone.Id);
                        } catch (NoRelevantPackageException ex) {
                            if (drone.BatteryStatus != 100) {
                                BaseStation closestStation = bl.GetBaseStation(Utils.ClosestStation(drone.Location, bl.dal.GetStationList()).Id);

                                if (drone.Location != closestStation.Location) {
                                    MoveNextStep(closestStation.Location);
                                } else if (closestStation.AvailableChargingSlots > 0) {
                                    bl.SendDroneToCharge(drone.Id);
                                }
                            }
                        }
                    } else if (drone.Status == DroneStatus.maintenance) {
                        if (drone.BatteryStatus != 100) {
                            SimulatorUpdateDroneBattery();
                        } else {
                            bl.ReleaseDroneFromCharge(droneId);
                        }
                    } else {
                        Package package = bl.GetPackage(drone.Package.Id);
                        if (package.CollectionTime is null) {
                            if (drone.Location == drone.Package.CollectionLoc) {
                                bl.CollectPackageByDrone(droneId);
                            }

                            MoveNextStep(drone.Package.CollectionLoc);
                        } else {
                            if (drone.Location == drone.Package.DeliveryLoc) {
                                bl.DeliverPackageByDrone(droneId);
                            }

                            MoveNextStep(drone.Package.DeliveryLoc);
                        }
                    }
                }
            }
        }
    }
}
