using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using Server.Api.Entities;

namespace Server.Api.Dtos
{

    /// <summary>
    /// DTO containing all questions in a survey
    /// </summary>
    public record SurveyQuestionsDto
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
        public ICollection<QuestionDto> Questions { get; init; }
    }
}