using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    public record Answer
    {
        public int Id { get; init; }

        [Required]
        public int LikertRating { get; init; }

        [Required]
        public SurveyAttempt Attempt { get; init; }

        [Required]
        public int AttemptId { get; init; }

        [Required]
        public Question Question { get; init; }

        [Required]
        public int QuestionId { get; init; }
    }
}