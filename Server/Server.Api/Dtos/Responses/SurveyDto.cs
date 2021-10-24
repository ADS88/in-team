using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using Server.Api.Entities;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO containing all information about a survey
    /// </summary>
    public record SurveyDto
    {
        [Required]
        public int Id { get; init; }
        
        [Required]
        public string Name { get; init; }

        [Required]
        public DateTimeOffset OpeningDate { get; init; }

        [Required]
        public DateTimeOffset ClosingDate { get; init; }

        [Required]
        public ICollection<State> States { get; init; }

        [Required]
        public ICollection<SimplifiedTeamDto> Teams { get; init; }

        [Required]
        public ICollection<Question> Questions { get; init; }

    }

    public record SimplifiedTeamDto {
        [Required]
        public int id;

        [Required]
        public string name;
    }
}