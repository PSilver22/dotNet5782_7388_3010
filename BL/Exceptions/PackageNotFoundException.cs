using System;

namespace IBL
{
    class PackageNotFoundException : Exception
    {
        public PackageNotFoundException(int id) : base($"No package found with ID {id}") { }
    }
}
