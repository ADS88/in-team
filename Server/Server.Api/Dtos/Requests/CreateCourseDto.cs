using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO to create a new course
    /// </summary>
    public record CreateCourseDto
    {
        [Required]
        public string Name { get; init; }
        
    }
}
