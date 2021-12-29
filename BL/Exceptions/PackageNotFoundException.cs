using System;

namespace BlApi
{
    public class PackageNotFoundException : Exception
    {
        public PackageNotFoundException(int id) : base($"No package found with ID {id}") { }
    }
}
