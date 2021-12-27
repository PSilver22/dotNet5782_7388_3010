using System;

namespace BlApi
{
    class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(int id) : base($"No customer found with ID {id}") { }
    }
}
