using System;

namespace BlApi
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(int id) : base($"No customer found with ID {id}") { }
    }
}
