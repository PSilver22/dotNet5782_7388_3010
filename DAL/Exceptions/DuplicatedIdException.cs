using System;

namespace DO
{
    public class DuplicatedIdException : Exception
    {
        public DuplicatedIdException(int id, string dataType) : base($"The {dataType} with {id} already exists in the database.") { }
    }
}
