using System.Collections;
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
        public DateTimeOffset OpeningDate { get; init; }

        [Required]
        public DateTimeOffset ClosingDate { get; init; }

        [Required]
        public DateTimeOffset CreatedDate { get; init; }

        [Required]
        public ICollection<Question> Questions { get; init; }

        [Required]
        public ICollection<Team> Teams { get; init; }

        [Required]
        public Iteration Iteration { get; init; }

        [Required]
        public int IterationId { get; init; }

        public ICollection<SurveyAttempt> Attempts { get; init;}

    }
}