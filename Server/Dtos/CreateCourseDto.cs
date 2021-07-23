using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Dtos
{
    public record CreateCourseDto
    {
        [Required]
        public string Name { get; init; }
        
    }
}
