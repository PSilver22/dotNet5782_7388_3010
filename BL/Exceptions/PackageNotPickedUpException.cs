using System;

namespace BlApi
{
    public class PackageNotPickedUpException : Exception
    {
        public PackageNotPickedUpException(int id) : base($"Drone has not yet picked up its assigned package") { }
    }
}
