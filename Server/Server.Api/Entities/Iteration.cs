using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Server.Api.Entities
{
    public record Iteration
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

        [Required]
        public Course Course { get; init; }

        public ICollection<Survey> Surveys { get; init; }

    }
}