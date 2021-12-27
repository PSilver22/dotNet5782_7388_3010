using System;

namespace BlApi
{
    class DroneNotChargingException : Exception
    {
        public DroneNotChargingException() : base("Drone is not currently charging") { }
    }
}
