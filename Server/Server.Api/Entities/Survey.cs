using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Entities
{
    public record Survey
    
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; init; }

        [Required]
        public DateTimeOffset StartDate { get; init; }

        [Required]
        public DateTimeOffset EndDate { get; init; }
        public ICollection<Question> Questions { get; init; }
    }
}