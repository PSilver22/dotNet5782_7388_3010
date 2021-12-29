using System;

namespace BlApi
{
    public class DroneNotFreeException : Exception
    {
        public DroneNotFreeException() : base("Drone is not free") { }

        public DroneNotFreeException(string attemptedAction) : base($"Cannot {attemptedAction}; drone is not currently free") { }
    }
}
