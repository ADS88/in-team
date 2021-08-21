using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Server.Api.Dtos
{
    public record CreateSurveyDto
    {

        [Required]
        public string Name { get; init; }

        [Required]
        public DateTimeOffset OpeningDate { get; init; }

        [Required]
        public DateTimeOffset ClosingDate { get; init; }

        [Required]
        public ICollection<int> StateIds { get; init; }

        [Required]
        public ICollection<int> TeamIds { get; init; }

    }
}