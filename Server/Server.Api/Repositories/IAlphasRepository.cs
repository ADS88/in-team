using Server.Api.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server.Api.Repositories
{
    /// <summary>
    /// Interface for Alpha database operations
    /// </summary>
    public interface IAlphasRepository
    {
        Task<IEnumerable<Alpha>> GetAlphas();
        Task<Alpha> GetAlpha(int id);
        Task<State> GetState(int stateId);
        Task AddAlpha(Alpha alpha);
        Task AddState(State state);
        Task AddQuestion(Question question);
        
    }
}