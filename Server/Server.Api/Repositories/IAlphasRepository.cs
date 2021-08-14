using Server.Api.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server.Api.Repositories
{
    public interface IAlphasRepository
    {
        Task<IEnumerable<Alpha>> GetAlphas();
        Task<IEnumerable<State>> GetStates(int alphaId);
        Task<IEnumerable<Question>> GetQuestions(int stateId);
        Task AddAlpha(Alpha alpha);
        Task AddState(State state);
        Task AddQuestion(Question question);
        
    }
}