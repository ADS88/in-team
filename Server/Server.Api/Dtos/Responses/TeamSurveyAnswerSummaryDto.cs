using System.Collections.Generic;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO containing the all information from a team answering surveys in an iteration
    /// </summary>
    public record TeamSurveyAnswerSummaryDto
    {
        public ICollection<TeamSurveyAlphaDto> Alphas { get; init; }

    }

    /// <summary>
    /// The average of a single question within the survey
    /// </summary>
    public record TeamSurveyQuestionDto
    {
        public string Content { get; init; }
        public double Average { get; init; }
    }

    /// <summary>
    /// The average of a single state within the survey
    /// </summary>
    public record TeamSurveyStateDto
    {
        public int StateId { get; init; }
        public string StateName { get; init; }
        public double Average { get; init; }
        public ICollection<TeamSurveyQuestionDto> AnswerSummaries { get; init; }
    }

    /// <summary>
    /// The averages of all states of an alpha within a survey
    /// </summary>
    public record TeamSurveyAlphaDto
    {
        public string AlphaName { get; init; }
        public int AlphaId { get; init; }
        public ICollection<TeamSurveyStateDto> States { get; init; }
    }

}