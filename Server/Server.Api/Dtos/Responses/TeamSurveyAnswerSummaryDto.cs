using System.Collections;
using System.Collections.Generic;

namespace Server.Api.Dtos
{
  public record TeamSurveyAnswerSummaryDto
  {
    public ICollection<TeamSurveyAlphaDto> Alphas { get; init; }

  }

  public record TeamSurveyQuestionDto {
    public string Content { get; init; }

    public double Average { get; init; }
  }

  public record TeamSurveyStateDto {
    public int StateId { get; init; }
    public string StateName { get; init; }
    public double Average { get; init; }
    public ICollection<TeamSurveyQuestionDto> SurveyQuestionDtos { get; init; }
  }

  public record TeamSurveyAlphaDto {
    public string AlphaName { get; init; }

    public int AlphaId { get; init; }

    public ICollection<TeamSurveyStateDto> States { get; init; }
  }

}