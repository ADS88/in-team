using System.Threading.Tasks;
using Server.Api.Data;
using Server.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Repositories
{
    public class SurveysRepository
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
            return await context.Surveys.FirstOrDefaultAsync(survey => survey.Id == id);
        }

    }
}
