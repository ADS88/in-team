using System.Collections;
using System.Linq;
using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
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
                CourseId = courseId,
                Points = 0
            };

            await repository.Add(team);
            return team;
        }

        public async Task AddMember(int teamId, string memberId){
            await repository.AddMember(teamId, memberId);
        }

        public async Task AchieveStates(AchievedStateDto dto, int teamId, int iterationid){
            var achievedStates = dto.AchievedStates.Select(achievedState => new AchievedState()
                {
                    TeamId = teamId,
                    IterationId = iterationid,
                    AlphaId = achievedState.AlphaId,
                    StateId = achievedState.StateId,
                    AchievedDate = DateTimeOffset.UtcNow
                });
            await repository.AchieveStates(achievedStates);
        }

        public async Task<AchievedStateResponseDto> GetTeamsCurrentStates(int teamId){
            var achievedStates = await repository.GetTeamsAchievedStates(teamId);
            Dictionary<int, List<AchievedState>> alphaToAchievedStates = new();
            foreach(var s in achievedStates){
                if(!alphaToAchievedStates.ContainsKey(s.AlphaId)){
                    alphaToAchievedStates[s.AlphaId] = new List<AchievedState>();
                }
                alphaToAchievedStates[s.AlphaId].Add(s);
            }
            List<AchievedStateResponse> result = new();
            foreach(var item in alphaToAchievedStates){
                var mostRecentStateAchieved = item.Value.Where(a => a.AchievedDate == item.Value.Max(achievedState => achievedState.AchievedDate))
                                                .FirstOrDefault();
                
                result.Add(new(){
                    Id = mostRecentStateAchieved.Id,
                    AlphaName = mostRecentStateAchieved.Alpha.Name,
                    StateName = mostRecentStateAchieved.State.Name,
                    AchievedDate = mostRecentStateAchieved.AchievedDate
                });
            }
            return new AchievedStateResponseDto(){AchievedStates = result};
        }
    }
}