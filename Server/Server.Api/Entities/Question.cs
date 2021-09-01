using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Entities
{
    public record Question
    {
        public int Id { get; init; }

        [Required]
        public String Content { get; init; }

        [Required]
        public State State { get; init; }

        [Required]
        public int StateId { get; init; }

        [Required]
        public DateTimeOffset CreatedDate { get; init; }

        public ICollection<Survey> Surveys { get; init; }

    }
}