using Server.Api.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server.Api.Services
{
    public interface ITeamService
    {
        Task<Team> GetById(int id);

        Task<Team> Create(string name, int courseId);
    }
}