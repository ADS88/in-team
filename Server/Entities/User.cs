using System;

namespace Server.Entities
{
    public record User
    {
        public Guid Id { get; init; }
        public String FirstName { get; init; }
        public String LastName { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
