using System;
using System.Collections.Generic;
using Server.Api.Entities;
using System.Threading.Tasks;

namespace Server.Api.Repositories
{
    public interface ITeamsRepository
    {
        Task<Team> Get(int id);
        Task<IEnumerable<Team>> GetAll();
        Task Add(Team course);
        Task Delete(int id);
        Task Update(Team team);
    }
}