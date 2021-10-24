using System;
namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO containing information about a user
    /// </summary>
    public record UserDto
    {
        public String Id { get; init; }
        public String FirstName { get; init; }
        public String LastName { get; init; }
        public String ProfileIcon { get; init; }

    }
}