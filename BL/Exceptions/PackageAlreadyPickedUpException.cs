using System;

namespace IBL
{
    class PackageAlreadyPickedUpException : Exception
    {
        public PackageAlreadyPickedUpException() : base("Drone has already picked up its assigned package") { }
    }
}
