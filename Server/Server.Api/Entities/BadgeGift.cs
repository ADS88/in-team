using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    /// <summary>
    /// Table storing badges gifted to users
    /// </summary>
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
