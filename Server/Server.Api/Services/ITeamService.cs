using Server.Api.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Server.Api.Dtos;

namespace Server.Api.Services
{
    /// <summary>
    /// An interface for business logic related to teams
    /// </summary>
    public interface ITeamService
    {
        Task<Team> GetById(int id);
        Task<IEnumerable<Team>> Get();
        Task<Team> Create(string name, int courseId);
        Task AddMember(int teamId, string memberId);
        Task<Boolean> DeleteTeam(int id);
        Task AchieveStates(AchievedStateDto dto, int teamId, int iterationid);
        Task<AchievedStateResponseDto> GetTeamsCurrentStates(int teamId);
        Task<TeamSurveyAnswerSummaryDto> GetTeamsSurveyAnswerSummaries(int teamId, int iterationId);
    }
}