using System.Collections.Generic;
using Server.Api.Entities;
using System.Threading.Tasks;

namespace Server.Api.Repositories
{
    /// <summary>
    /// Interface for team database operations
    /// </summary>
    public interface ITeamsRepository
    {
        Task<Team> Get(int id);
        Task<IEnumerable<Team>> GetAll();
        Task Add(Team team);
        Task Delete(int id);
        Task AddMember(int teamId, string memberId);
        Task AddPoints(int teamId, int points);
        Task AchieveStates(IEnumerable<AchievedState> achievedStates);
        Task<IEnumerable<AchievedState>> GetTeamsAchievedStates(int teamId);
        Task<ICollection<SurveyAttempt>> GetTeamsSurveyAnswerSummaries(int teamId, int iterationId);
    }
}