using System;

namespace BlApi
{
    public class DroneNotChargingException : Exception
    {
        public DroneNotChargingException() : base("Drone is not currently charging") { }
    }
}
