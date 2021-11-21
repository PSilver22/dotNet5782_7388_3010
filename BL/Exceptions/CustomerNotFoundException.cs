using System;

namespace IBL
{
    class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(int id) : base($"No customer found with ID {id}") { }
    }
}
