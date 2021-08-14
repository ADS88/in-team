using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Dtos
{
    public record CreateCourseDto
    {
        [Required]
        public string Name { get; init; }
        
    }
}
