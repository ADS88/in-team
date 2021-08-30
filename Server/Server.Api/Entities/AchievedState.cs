using System.ComponentModel.DataAnnotations;
using System;

namespace Server.Api.Entities
{
    public record AchievedState
    {
         public int Id { get; init; }

        [Required]
        public State State { get; init; }

        [Required]
        public int StateId { get; init; }

        [Required]
        public Alpha Alpha { get; init; }

        [Required]
        public int AlphaId { get; init; }

        [Required]
        public DateTimeOffset AchievedDate { get; init; }

        [Required]
        public Iteration Iteration { get; init; }

        [Required]
        public int IterationId { get; init; }

        [Required]
        public Team Team { get; init; }

        [Required]
        public int TeamId { get; init; }

    }
}