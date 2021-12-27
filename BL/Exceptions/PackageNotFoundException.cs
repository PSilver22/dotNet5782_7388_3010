using System;

namespace BlApi
{
    class PackageNotFoundException : Exception
    {
        public PackageNotFoundException(int id) : base($"No package found with ID {id}") { }
    }
}
