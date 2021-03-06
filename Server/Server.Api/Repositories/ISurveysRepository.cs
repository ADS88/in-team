using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Api.Entities;

namespace Server.Api.Repositories
{
    /// <summary>
    /// Interface for survey database operations
    /// </summary>
    public interface ISurveysRepository
    {
        Task Create(Survey survey);
        Task<Survey> Get(int id);
        Task<IEnumerable<Survey>> GetAll();
        Task CreateSurveyAttempt(SurveyAttempt attempt);
        Task AddAnswers(IEnumerable<Answer> answers);
        Task AddBadgeGifts(IEnumerable<BadgeGift> badgeGifts);
        Task<SurveyAttempt> GetAttempt(int surveyId, string userId);
        Task<IEnumerable<Survey>> GetSurveysAssignedToStudent(AppUser user);
        Task<IEnumerable<SurveyAttempt>> GetAttemptsFromUser(string userId);
        Task<IEnumerable<Badge>> GetBadges();
        Task<Survey> GetSurveyWithTeams(int surveyId);
    }
}
