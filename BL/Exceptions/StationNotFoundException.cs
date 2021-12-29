using System;

namespace BlApi
{
    public class StationNotFoundException : Exception
    {
        public StationNotFoundException(int id) : base($"No station found with ID {id}") { }
    }
}
