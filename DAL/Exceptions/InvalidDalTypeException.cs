using System;

namespace DalApi
{
    public class InvalidDalTypeException : Exception
    {
        public InvalidDalTypeException(string type) : base($"The requested DAL type \"{type}\" is not valid.") { }
    }
}