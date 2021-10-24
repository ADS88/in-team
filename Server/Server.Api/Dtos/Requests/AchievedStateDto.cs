using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO holding the result of the teaching team grading an iteration
    /// </summary>
    public record AchievedStateDto
    {
      [Required]
      public ICollection<SingleAchievedState> AchievedStates { get; init; }

      public int Points { get; init; }
    }

    /// <summary>
    /// The state achieved for each alpha
    /// </summary>
    public record SingleAchievedState 
    {
      [Required]
      public int StateId { get; init; }

      [Required]
      public int AlphaId { get; init; }
    }

}