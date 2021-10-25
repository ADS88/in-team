using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Api.Data;
using Server.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Repositories
{
    /// <summary>
    /// Repository class for survey database operations
    /// </summary>
    public class SurveysRepository : ISurveysRepository
    {
        private readonly IDataContext context;

        public SurveysRepository(IDataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Creates a survey in the database
        /// </summary>
        /// <param name="survey">The survey to be created</param>
        /// <returns></returns>
        public async Task Create(Survey survey)
        {
            context.Surveys.Add(survey);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a survey by ID from the database
        /// </summary>
        /// <param name="id">The ID of the survey to get</param>
        /// <returns>The first survey with that id, or null if none exist</returns>
        public async Task<Survey> Get(int id)
        {
            return await context.Surveys.Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// Gets all the surveys from the database
        /// </summary>
        /// <returns>A list of all surveys in the database</returns>
        public async Task<IEnumerable<Survey>> GetAll()
        {
            return await context.Surveys
                .Include(s => s.Teams)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a survey in the database, including their linked teams
        /// </summary>
        /// <param name="id">The ID of the survey to get</param>
        /// <returns>A list of surveys with that ID, and their related teams</returns>
        public async Task<Survey> GetSurveyWithTeams(int id)
        {
            return await context.Surveys.Include(s => s.Teams).ThenInclude(t => t.Members).FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// Saves a students survey answers and badges given to the database
        /// </summary>
        /// <param name="attempt">A students survey answers and badges given</param>
        /// <returns></returns>
        public async Task CreateSurveyAttempt(SurveyAttempt attempt)
        {
            context.SurveyAttempts.Add(attempt);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Saves a students survey question answers to the database
        /// </summary>
        /// <param name="answers">A list of answers to the survey (likert scale)</param>
        /// <returns></returns>
        public async Task AddAnswers(IEnumerable<Answer> answers)
        {
            context.Answers.AddRange(answers);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Saves the badges a student has giften others to the database
        /// </summary>
        /// <param name="badgeGifts">The badges a student has gifted</param>
        /// <returns></returns>
        public async Task AddBadgeGifts(IEnumerable<BadgeGift> badgeGifts)
        {
            context.BadgeGifts.AddRange(badgeGifts);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a specific survey attempt from a user in the database
        /// </summary>
        /// <param name="surveyId">The ID of the survey</param>
        /// <param name="userId">The ID of the user who has attempted the survey</param>
        /// <returns>The attempt, or null if they have not attempted the survey</returns>
        public async Task<SurveyAttempt> GetAttempt(int surveyId, string userId)
        {
            var attempt = await context.SurveyAttempts.FirstOrDefaultAsync(attempt => attempt.Id == surveyId && attempt.AppUser.Id == userId);
            return attempt;
        }

        /// <summary>
        /// Gets all the surveys a student needs to complete from the database
        /// </summary>
        /// <param name="user">The user to be checked</param>
        /// <returns>An enumerable of surveys the student needs to complete</returns>
        public async Task<IEnumerable<Survey>> GetSurveysAssignedToStudent(AppUser user)
        {
            var surveys = await context.Surveys.
                Include(s => s.Teams)
                .ToListAsync();

            ICollection<Survey> usersSurveys = new List<Survey>();
            foreach (var survey in surveys)
            {
                foreach (var team in user.Teams)
                {
                    if (survey.Teams.Contains(team))
                    {
                        usersSurveys.Add(survey);
                        break;
                    }
                }
            }
            return usersSurveys;
        }

        /// <summary>
        /// Gets all badges within the database
        /// </summary>
        /// <returns>An enumerable of badges</returns>
        public async Task<IEnumerable<Badge>> GetBadges()
        {
            return await context.Badges.ToListAsync();
        }

        /// <summary>
        /// Gets all survey attempts from a specific user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>A list of all survey answers that the user has made</returns>
        public async Task<IEnumerable<SurveyAttempt>> GetAttemptsFromUser(string userId)
        {
            var attempts = await context.SurveyAttempts
                .Include(s => s.AppUser)
                .ToListAsync();
            return attempts.Where(s => s.AppUser.Id == userId);


        }
    }
}
