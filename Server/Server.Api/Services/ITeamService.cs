using Server.Api.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Server.Api.Services
{
    public interface ITeamService
    {
        Task<Team> GetById(int id);
        Task<IEnumerable<Team>> Get();
        Task<Team> Create(string name, int courseId);
        Task AddMember(int teamId, string memberId);
        Task<Boolean> DeleteTeam(int id);
    }
}