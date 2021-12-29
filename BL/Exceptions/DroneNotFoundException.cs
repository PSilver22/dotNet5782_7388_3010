using System;

namespace BlApi
{
    public class DroneNotFoundException : Exception
    {
        public DroneNotFoundException(int id) : base($"No drone found with ID {id}") { }
    }
}
