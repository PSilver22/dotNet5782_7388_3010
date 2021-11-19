using System;

namespace IDAL
{
    namespace DO
    {
        public class InvalidIdException : Exception
        {
            public InvalidIdException() : base() { }

            public InvalidIdException(string message) : base("Invalid ID Exception: " + message) { }

            public InvalidIdException(string message, Exception innerException) : base("Invalid ID Exception: " + message, innerException) { }

            public InvalidIdException(int id) : this($"{id} is not valid.") { }
        }
    }
}
