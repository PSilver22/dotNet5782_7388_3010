using System;

namespace DAL.Exceptions
{
    public class InvalidXMLException : Exception
    {
        public InvalidXMLException() : base("Failed to load data from XML file") {}
    }
}