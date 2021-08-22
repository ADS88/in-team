using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    public record SurveyAttempt
    {
        public int Id { get; init; }

        [Required]
        public AppUser AppUser { get; init; }

        [Required]
        public int SurveyId { get; init; }

        [Required]
        public Survey Survey { get; init; }

        public DateTimeOffset CompletedDate { get; init; }

        public ICollection<Answer> Answers { get; init; }

        public Boolean IsCompleted
        {
            get
            {
                return CompletedDate != null ;
            }
        }
    }
}