using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    /// <summary>
    /// Table storing users answers to surveys
    /// </summary>
    public record SurveyAttempt
    {
        public int Id { get; init; }

        [Required]
        public AppUser AppUser { get; init; }

        [Required]
        public int SurveyId { get; init; }

        [Required]
        public Survey Survey { get; init; }

        [Required]
        public DateTimeOffset CompletedDate { get; init; }

        public ICollection<Answer> Answers { get; init; }

        public ICollection<BadgeGift> BadgeGifts { get; init; }

    }
}