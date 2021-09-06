using System.ComponentModel.DataAnnotations;
using System;

namespace Server.Api.Dtos
{
    public record IterationDto
    {
         public int Id { get; init; }

        [Required]
        public String Name { get; init; }
        [Required]
        public DateTimeOffset CreatedDate { get; init; }

        [Required]
        public DateTimeOffset StartDate { get; init; }

        [Required]
        public DateTimeOffset EndDate { get; init; }

        [Required]
        public int CourseId { get; init; }

        public string CourseName { get; init; }

    }
}