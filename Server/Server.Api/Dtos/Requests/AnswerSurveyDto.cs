using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Server.Api.Dtos
{
    /// <summary>
    /// Contains a student's survey answers and badges gifted
    /// </summary>
    public record AnswerSurveyDto
    {
      [Required]
      public ICollection<SingleAnswer> Answers { get; init; }

      [Required]
      public ICollection<BadgeGiftDto> BadgeGifts { get; init; }

    }

    /// <summary>
    /// A single survey question's answer
    /// </summary>
    public record SingleAnswer {

      [Required]
      public int QuestionId { get; init; }

      [Required]
      [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
      public int LikertRating { get; init; }
    }

    /// <summary>
    /// A badge gifted while answering the survey
    /// </summary>
    public record BadgeGiftDto {

      [Required]
      public int BadgeId { get; init; }

      [Required]
      public string UserId { get; init; }
    }
}