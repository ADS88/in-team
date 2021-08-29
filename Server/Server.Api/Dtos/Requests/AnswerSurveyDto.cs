using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Server.Api.Dtos
{
    public record AnswerSurveyDto
    {
      [Required]
      public ICollection<SingleAnswer> Answers { get; init; }

      [Required]
      public ICollection<BadgeGiftDto> BadgeGifts { get; init; }

    }

    public record SingleAnswer {

      [Required]
      public int QuestionId { get; init; }

      [Required]
      [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
      public int LikertRating { get; init; }
    }

    public record BadgeGiftDto {

      [Required]
      public int BadgeId { get; init; }

      [Required]
      public string UserId { get; init; }
    }
}