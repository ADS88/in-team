using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Dtos
{

    /// <summary>
    /// DTO to update a students profile icon
    /// </summary>
    public record UpdateProfileIconDto
    {
        [Required]
        public string ProfileIcon { get; init; }
        
    }
}
