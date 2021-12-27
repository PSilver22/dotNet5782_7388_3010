using System;

namespace BlApi
{
    class DroneNotFoundException : Exception
    {
        public DroneNotFoundException(int id) : base($"No drone found with ID {id}") { }
    }
}
