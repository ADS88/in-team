using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections;


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
        public async Task<Team> GetById(int id){
            return await repository.Get(id);
        }

        public async Task<Team> Create(string name, ICollection<string> membersIds){
            var members = new List<AppUser>();
            if(membersIds is not null){
                foreach(var memberId in membersIds){
                    var member = await userManager.FindByIdAsync(memberId);
                    members.Add(member);
                }
            }
            Team team = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow,
                Members = members
            };
            await repository.Add(team);
            return team;
        }
    }
}