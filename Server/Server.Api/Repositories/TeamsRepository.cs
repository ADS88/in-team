using System;
using System.Collections.Generic;
using Server.Api.Entities;
using Server.Api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Repositories
{
    public class TeamsRepository : ITeamsRepository
    {
        private readonly IDataContext context;
        public TeamsRepository(IDataContext context){
            this.context = context;
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            return await context.Teams.ToListAsync();
        }

        public async Task<Team> Get(int id)
        {
            return await context.Teams.Include(team => team.Members).FirstOrDefaultAsync(team => team.Id == id);
        }

        public async Task Add(Team team)
        {
            context.Teams.Add(team);
            await context.SaveChangesAsync();
        }

        public async Task Update(Team team)
        {
            var teamToUpdate = await context.Courses.FindAsync(team.Id);
            if(teamToUpdate == null){
                throw new NullReferenceException();
            }

            //TODO: Update work here

        }

        public async Task Delete(int id)
        {
            var teamToDelete = await context.Teams.FindAsync(id);
            if(teamToDelete == null){
                throw new NullReferenceException();
            }
            context.Teams.Remove(teamToDelete);;
            await context.SaveChangesAsync();
        }
    }
}
