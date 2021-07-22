using System;
namespace Server.Dtos
{
    public record UserDto
    {
        public Guid Id { get; init; }
        public String FirstName { get; init; }
        public String LastName { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
