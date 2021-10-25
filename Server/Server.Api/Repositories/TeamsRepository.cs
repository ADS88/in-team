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
    /// <summary>
    /// Repository class for team database operations
    /// </summary>
    public class TeamsRepository : ITeamsRepository
    {
        private readonly IDataContext context;

        private readonly UserManager<AppUser> userManager;
        public TeamsRepository(IDataContext context, UserManager<AppUser> userManager){
            this.context = context;
            this.userManager = userManager;
        }

        /// <summary>
        /// Gets all teams in the database
        /// </summary>
        /// <returns>An enumerable of teams</returns>
        public async Task<IEnumerable<Team>> GetAll()
        {
            return await context.Teams.ToListAsync();
        }

        /// <summary>
        /// Gets a team by ID from the database
        /// </summary>
        /// <param name="id">The ID of the team to get</param>
        /// <returns>The first team with that ID, or null if no teams are found</returns>
        public async Task<Team> Get(int id)
        {
            return await context.Teams.Include(team => team.Members).FirstOrDefaultAsync(team => team.Id == id);
        }

        /// <summary>
        /// Adds a new team to the database
        /// </summary>
        /// <param name="team">The team to add</param>
        /// <returns></returns>
        public async Task Add(Team team)
        {
            context.Teams.Add(team);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a team from the database
        /// </summary>
        /// <param name="id">The ID of the team to delete</param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var teamToDelete = await context.Teams.FindAsync(id);
            context.Teams.Remove(teamToDelete);;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds a member to a team in the database
        /// </summary>
        /// <param name="teamId">The ID of the team to add to</param>
        /// <param name="memberId">The ID of the team member being added</param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds a specific number of points to a team in the database
        /// </summary>
        /// <param name="teamId">The ID of the team to add points to</param>
        /// <param name="points">The number of points to add</param>
        /// <returns></returns>
        public async Task AddPoints(int teamId, int points){
            var team = await context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
            team.Points += points;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Used by lecturers when grading, updates what states a team is currently achieving
        /// </summary>
        /// <param name="achievedStates">The states the team has most recently achieved</param>
        /// <returns></returns>
        public async Task AchieveStates(IEnumerable<AchievedState> achievedStates){
            context.AchievedStates.AddRange(achievedStates);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the states achieved by a team for each alpha
        /// </summary>
        /// <param name="teamId">The ID of the team to get states for</param>
        /// <returns>An enumerable of all states the team has currently achieved</returns>
        public async Task<IEnumerable<AchievedState>> GetTeamsAchievedStates(int teamId){
            var achievedStates = await context.AchievedStates
                                        .Include(achievedState =>achievedState.Alpha)
                                        .Include(achievedState => achievedState.Team)
                                        .Include(achievedState => achievedState.State)
                                        .Where(state => state.TeamId == teamId)
                                        .ToListAsync();
            return achievedStates;
        }

        /// <summary>
        /// Gets an average of the teams answers over an iteration
        /// </summary>
        /// <param name="teamId">The ID of the team</param>
        /// <param name="iterationId">The ID of the iteration to get averages from</param>
        /// <returns>The average of each state and question for each Alpha in the survey</returns>
        public async Task<ICollection<SurveyAttempt>> GetTeamsSurveyAnswerSummaries(int teamId, int iterationId){
            return await context.SurveyAttempts
                        .Include(attempt => attempt.Survey)
                        .ThenInclude(survey => survey.Questions)
                        .ThenInclude(question => question.State)
                        .ThenInclude(state => state.Alpha)
                        .Include(attempt => attempt.Answers)
                        .Where(attempt => attempt.Survey.IterationId == iterationId && attempt.Survey.Teams.Select(t => t.Id).Contains(teamId))
                        .ToListAsync();
        }
    }
}
