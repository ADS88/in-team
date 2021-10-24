using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    /// <summary>
    /// Table holding survey answers
    /// </summary>
    public record Answer
    {
        public int Id { get; init; }

        [Required]
        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
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