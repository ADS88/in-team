using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Api.Data;
using Server.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Repositories
{
    public class SurveysRepository: ISurveysRepository
    {
        private readonly IDataContext context;

        public SurveysRepository(IDataContext context)
        {
            this.context = context;
        }

        public async Task Create(Survey survey)
        {
            context.Surveys.Add(survey);
            await context.SaveChangesAsync(); 
        }

        public async Task<Survey> Get(int id)
        {
            return await context.Surveys.Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Survey>> GetAll()
        {
            return await context.Surveys.Include(s => s.Teams).ToListAsync();
        }

        public async Task CreateSurveyAttempt(SurveyAttempt attempt){
            context.SurveyAttempts.Add(attempt);
            await context.SaveChangesAsync();
        }

        public async Task AddAnswers(IEnumerable<Answer> answers){
            context.Answers.AddRange(answers);
            await context.SaveChangesAsync();
        }

        public async Task<SurveyAttempt> GetAttempt(int surveyId, string userId){
            var attempt = await context.SurveyAttempts.FirstOrDefaultAsync(attempt => attempt.Id == surveyId && attempt.AppUser.Id == userId);
            return attempt;
        }

    }
}
