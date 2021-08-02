using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Dtos
{
    public record CreateTeamDto
    {
        [Required]
        public string Name { get; init; }

        public ICollection<string> Members { get; init; }
        
    }
}
