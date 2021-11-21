using System;

namespace IBL
{
    class DroneNotFoundException : Exception
    {
        public DroneNotFoundException(int id) : base($"No drone found with ID {id}") { }
    }
}
