using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO showing what states a team has achieved for an iteration
    /// </summary>
    public record AchievedStateResponseDto
    {
      [Required]
      public ICollection<AchievedStateResponse> AchievedStates { get; init; }
    }

    /// <summary>
    /// DTO containing the state of a single Alpha
    /// </summary>
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