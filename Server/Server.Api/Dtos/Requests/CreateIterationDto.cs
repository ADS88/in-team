using System.ComponentModel.DataAnnotations;
using System;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO to create a new iteration within a course.
    /// </summary>
    public record CreateIterationDto
    {

        [Required]
        public String Name { get; init; }

        [Required]
        public DateTimeOffset StartDate { get; init; }

        [Required]
        public DateTimeOffset EndDate { get; init; }

    }
}