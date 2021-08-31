using System.Collections.Generic;
using Server.Api.Entities;
using System.Threading.Tasks;

namespace Server.Api.Repositories
{
    public interface ITeamsRepository
    {
        Task<Team> Get(int id);
        Task<IEnumerable<Team>> GetAll();
        Task Add(Team team);
        Task Delete(int id);
        Task AddMember(int teamId, string memberId);
        Task AddPoints(int teamId, int points);
        Task AchieveStates(IEnumerable<AchievedState> achievedStates);
    }
}