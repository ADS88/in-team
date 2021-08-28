using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Dtos
{
    public record UpdateProfileIconDto
    {
        [Required]
        public string ProfileIcon { get; init; }
        
    }
}
