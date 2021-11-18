using System;

namespace IBL
{
    class DroneNotAssignedPackageException : Exception
    {
        public DroneNotAssignedPackageException() : base("Drone has not yet been assigned a package") { }
    }
}
