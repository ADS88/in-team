using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Server.Api.Services
{
    public class TeamService: ITeamService
    {
        private readonly ITeamsRepository repository;
        private readonly UserManager<AppUser> userManager;

        public TeamService(ITeamsRepository repository, UserManager<AppUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<Team>> Get(){
            return await repository.GetAll();
        }
        public async Task<Team> GetById(int id){
            return await repository.Get(id);
        }

        public async Task<Boolean> DeleteTeam(int id){
            var team = await GetById(id);
            if(team is null){
                return false;
            }
            await repository.Delete(id);
            return true;
        }

        public async Task<Team> Create(string name, int courseId){
            Team team = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow,
                CourseId = courseId
            };

            await repository.Add(team);
            return team;
        }

        public async Task AddMember(int teamId, string memberId){
            await repository.AddMember(teamId, memberId);
        }
    }
}