using static System.StringComparison;
using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Server.Api.Dtos;

namespace Server.Api.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository repository;
        
        private readonly ISurveysRepository surveysRepository;

        private readonly ITeamsRepository teamsRepository;

        public UserService(IUserRepository repository, ISurveysRepository surveysRepository, ITeamsRepository teamsRepository)
        {
            this.repository = repository;
            this.surveysRepository = surveysRepository;
            this.teamsRepository = teamsRepository;
        }
        public async Task<IEnumerable<AppUser>> GetAll(){
            return await repository.GetAll();
        }
        public async Task<IEnumerable<AppUser>> GetEligibleForCourse(int teamId, string search){
            var allUsers = await repository.GetAllWithTeams();
            var courseId = (await teamsRepository.Get(teamId)).CourseId;
            return allUsers.Where(user => FilterByName(user, search))
            .Where(user => EligibleForCourse(user, courseId));
        }
        public async Task UpdateProfileIcon(string userId, string newIcon){
            await repository.UpdateProfileIcon(userId, newIcon);
        }

        public async Task<IEnumerable<UserBadgeDto>> GetBadges(string userId){
            var badgeGifts = await repository.GetBadgeGifts(userId);
            var allBadges = await surveysRepository.GetBadges();
            var badgeCounts = new Dictionary<Badge, int>();
            var userBadgeDtos = new List<UserBadgeDto>();
            foreach(var badge in allBadges){
                badgeCounts.Add(badge, 0);
            }
            foreach(var badgeGift in badgeGifts){
                badgeCounts[badgeGift.Badge] += 1;
            }
            foreach(var item in badgeCounts){
                userBadgeDtos.Add(new(){Count = item.Value, Id = item.Key.Id, Name = item.Key.Name});
            }
            return userBadgeDtos;
        }

        public async Task<AppUser> GetFullDetails(string userId){
            return await repository.GetUserWithTeamsAndMembers(userId);
        }

        private Boolean EligibleForCourse(AppUser user, int courseId){
            return !user.Teams.Any(team => team.CourseId == courseId);
        }

        private Boolean FilterByName(AppUser user, string search){
            return user.LastName.Contains(search, StringComparison.CurrentCultureIgnoreCase) || user.FirstName.Contains(search, StringComparison.CurrentCultureIgnoreCase);
        }

    }
}