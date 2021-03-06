using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Api.Entities;
using Server.Api.Dtos;

namespace Server.Api.Services
{
    /// <summary>
    /// An interface for business logic related to surveys
    /// </summary>
    public interface ISurveyService
    {
        Task<Survey> Create(string name, ICollection<int> stateIds, ICollection<int> teamIds, DateTimeOffset start, DateTimeOffset end, int iterationId);
        Task<Survey> Get(int id);
        Task<IEnumerable<Survey>> GetAll();
        Task<SurveyAttempt> AnswerSurvey(AnswerSurveyDto dto, int surveyId, string userId);
        Task<IEnumerable<Survey>> GetSurveysStudentNeedsToComplete(string userId);
        Task<IEnumerable<Badge>> GetBadges();
        Task<IEnumerable<AppUser>> FindTeamMembersFromSurvey(int surveyId, string userId);
    }
}