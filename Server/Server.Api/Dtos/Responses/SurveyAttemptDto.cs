using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Dtos
{
    public record SurveyAttemptDto
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public int SurveyId { get; init; }

        public DateTimeOffset CompletedDate { get; init; }

    }
}