using System;
using System.Collections.Generic;
using Server.Api.Entities;
using Server.Api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Server.Api.Repositories
{
    public class TeamsRepository : ITeamsRepository
    {
        private readonly IDataContext context;

        private readonly UserManager<AppUser> userManager;
        public TeamsRepository(IDataContext context, UserManager<AppUser> userManager){
            this.context = context;
            this.userManager = userManager;
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

        public async Task Delete(int id)
        {
            var teamToDelete = await context.Teams.FindAsync(id);
            context.Teams.Remove(teamToDelete);;
            await context.SaveChangesAsync();
        }

        public async Task AddMember(int teamId, string memberId)
        {
            var team = await context.Teams.Include(t => t.Members).FirstOrDefaultAsync(t => t.Id == teamId);
            if(team is null){
                throw new NullReferenceException();
            }
            var user = await userManager.FindByIdAsync(memberId);
            
            if(user is null){
                throw new NullReferenceException();
            }
            team.Members.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task AddPoints(int teamId, int points){
            var team = await context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
            team.Points += points;
            await context.SaveChangesAsync();
        }

        public async Task AchieveStates(IEnumerable<AchievedState> achievedStates){
            context.AchievedStates.AddRange(achievedStates);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AchievedState>> GetTeamsAchievedStates(int teamId){
            var achievedStates = await context.AchievedStates
                                        .Include(achievedState =>achievedState.Alpha)
                                        .Include(achievedState => achievedState.Team)
                                        .ToListAsync();
            return achievedStates.Where(state => state.TeamId == teamId);
        }
    }
}
