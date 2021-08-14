using Server.Api.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server.Api.Services
{
    public interface IAlphaService
    {
        Task<IEnumerable<Alpha>> GetAlphas();
        Task<IEnumerable<State>> GetStates(int alphaId);
        Task<IEnumerable<Question>> GetQuestions(int stateId);
        Task<Alpha> CreateAlpha(string name);
        Task<State> AddState(string name, int alphaId);
        Task<Question> AddQuestion(string content, int stateId);
    }
}