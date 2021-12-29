#nullable enable

using System;

namespace BlApi
{
    public class DuplicatedIdException : Exception
    {
        public DuplicatedIdException(int id, string? entityName) : base($"A(n) {entityName ?? "entity"} with ID {id} already exists") { }
    }
}
