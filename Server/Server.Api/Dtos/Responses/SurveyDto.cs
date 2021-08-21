using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using Server.Api.Entities;

namespace Server.Api.Dtos
{
    public record SurveyDto
    {

        [Required]
        public string Name { get; init; }

        [Required]
        public DateTimeOffset OpeningDate { get; init; }

        [Required]
        public DateTimeOffset ClosingDate { get; init; }

        [Required]
        public ICollection<State> States { get; init; }

        [Required]
        public ICollection<Team> Teams { get; init; }

    }
}