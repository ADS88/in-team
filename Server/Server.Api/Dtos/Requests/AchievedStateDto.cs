using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Dtos
{
    public record AchievedStateDto
    {
      [Required]
      public ICollection<SingleAchievedState> AchievedStates { get; init; }

      public int Points { get; init; }
    }

    public record SingleAchievedState 
    {
      [Required]
      public int StateId { get; init; }

      [Required]
      public int AlphaId { get; init; }
    }

}