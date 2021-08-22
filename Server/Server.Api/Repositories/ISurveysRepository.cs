using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Api.Entities;

namespace Server.Api.Repositories
{
    public interface ISurveysRepository
    {
        Task Create(Survey survey);
        Task<Survey> Get(int id);
        Task<IEnumerable<Survey>> GetAll();
        Task CreateSurveyAttempt(SurveyAttempt attempt);
        Task AddAnswers(IEnumerable<Answer> answers);
    }
}
