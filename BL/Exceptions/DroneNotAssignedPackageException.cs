using System;

namespace BlApi
{
    public class DroneNotAssignedPackageException : Exception
    {
        public DroneNotAssignedPackageException() : base("Drone has not yet been assigned a package") { }
    }
}
