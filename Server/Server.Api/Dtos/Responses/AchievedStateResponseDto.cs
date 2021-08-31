using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Dtos
{
    public record AchievedStateResponseDto
    {
      [Required]
      public ICollection<AchievedStateResponse> AchievedStates { get; init; }
    }

    public record AchievedStateResponse
    {

      [Required]
      public int Id { get; init; }

      [Required]
      public string AlphaName { get; init; }

      [Required]
      public string StateName { get; init; }

      [Required]
      public DateTimeOffset AchievedDate { get; init; }
    }

}