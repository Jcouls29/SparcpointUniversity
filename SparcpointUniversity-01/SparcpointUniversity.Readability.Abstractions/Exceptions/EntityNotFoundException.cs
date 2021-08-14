using System;

namespace SparcpointUniversity.Readability.Abstractions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityType) : base($"{entityType} not found.") { }
    }
}
