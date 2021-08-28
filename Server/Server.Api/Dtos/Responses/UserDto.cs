using System;
namespace Server.Api.Dtos
{
     public record UserDto
    {
        public String Id { get; init; }
        public String FirstName { get; init; }
        public String LastName { get; init; }

        public String ProfileIcon { get; init; }

    }
}