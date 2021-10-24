using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Entities
{
    /// <summary>
    /// Table storing questions to know if a state of an alpha has been achieved
    /// </summary>
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