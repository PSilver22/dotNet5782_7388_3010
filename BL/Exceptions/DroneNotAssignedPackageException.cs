using System;

namespace BlApi
{
    class DroneNotAssignedPackageException : Exception
    {
        public DroneNotAssignedPackageException() : base("Drone has not yet been assigned a package") { }
    }
}
