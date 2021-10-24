using System;
using System.Collections.Generic;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO containing information about a team
    /// </summary>
    public record TeamDto
    {
        public int Id { get; init; }
        public String Name { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public ICollection<UserDto> Members { get; init; }
        public int Points { get; init; }
    }
}
