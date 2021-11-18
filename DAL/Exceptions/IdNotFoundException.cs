using System;

namespace IDAL
{
    namespace DO
    {
        class IdNotFoundException : Exception
        {
            public IdNotFoundException() : base() { }

            public IdNotFoundException(string message) : base("ID Not Found Exception: " + message) { }

            public IdNotFoundException(string message, Exception innerException) : base("ID Not Found Exception: " + message, innerException) { }

            public IdNotFoundException(int id) : this($"The ID {id} was not found in the database.") { }
        }
    }
}
