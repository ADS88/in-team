using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Entities
{
    public record BadgeGift
    {
        public int Id { get; init; }

        [Required]
        public Badge Badge { get; init; }

        [Required]
        public int BadgeId { get; init; }

        [Required]
        public AppUser User { get; init; }

        [Required]
        public SurveyAttempt SurveyAttempt { get; init; }

        [Required]
        public int SurveyAttemptId { get; init; }
    }
}
