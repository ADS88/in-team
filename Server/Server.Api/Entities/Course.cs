using System;

namespace Server.Api.Entities
{
    public record Course
    {
        public int Id { get; init; }
        public String Name { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
