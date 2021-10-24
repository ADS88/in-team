using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO to create a new team of students within a course
    /// </summary>
    public record CreateTeamDto
    {
        [Required]
        public string Name { get; init; }

        public ICollection<string> Members { get; init; }

        [Required]
        public int CourseId { get; init; }
        
    }
}
