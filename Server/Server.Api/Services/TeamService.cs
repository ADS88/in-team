using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using Server.Api.Dtos;


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