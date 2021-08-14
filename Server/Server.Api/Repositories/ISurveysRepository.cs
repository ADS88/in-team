using System.Threading.Tasks;
using Server.Api.Entities;

namespace Server.Api.Repositories
{
    public interface ISurveysRepository
    {
        Task Create(Survey survey);

        Task<Survey> Get(int id);
    }
}
