using Server.Api.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server.Api.Services
{
    public interface IAlphaService
    {
        Task<IEnumerable<Alpha>> GetAlphas();
        Task<Alpha> GetAlpha(int id);
        Task<State> GetState(int id);
        Task<Alpha> CreateAlpha(string name);
        Task<State> AddState(string name, int alphaId);
        Task<Question> AddQuestion(string content, int stateId);
    }
}