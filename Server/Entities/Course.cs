using System;

namespace Server.Entities
{
    public record Course
    {
        public int Id { get; init; }
        public String Name { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
