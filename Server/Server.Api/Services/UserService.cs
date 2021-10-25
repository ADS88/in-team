using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Server.Api.Dtos;

namespace Server.Api.Services
{
    /// <summary>
    /// Business logic for users within the application
    /// </summary>
    public class UserService : IUserService
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

        /// <summary>
        /// Gets all users of the application
        /// </summary>
        /// <returns>An enumerable of users</returns>
        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await repository.GetAll();
        }

        /// <summary>
        /// Gets all users that can be added to a team, used for autocomplete.
        /// </summary>
        /// <param name="teamId">The ID of the team we are seeing users eligible for</param>
        /// <param name="search">The search string entered to filter users by</param>
        /// <returns>An enumerable of users that can be added to a course</returns>
        public async Task<IEnumerable<AppUser>> GetEligibleForCourse(int teamId, string search)
        {
            var allUsers = await repository.GetAllWithTeams();
            var courseId = (await teamsRepository.Get(teamId)).CourseId;
            return allUsers.Where(user => FilterByName(user, search))
            .Where(user => EligibleForCourse(user, courseId));
        }

        /// <summary>
        /// Updates a users profile icon
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="newIcon">The new profile icon for the user</param>
        /// <returns></returns>
        public async Task UpdateProfileIcon(string userId, string newIcon)
        {
            await repository.UpdateProfileIcon(userId, newIcon);
        }

        /// <summary>
        /// Get the badges that have been awarded to a user
        /// </summary>
        /// <param name="userId">The ID of the user we are getting badges for</param>
        /// <returns>An enumerable of badges and their corresponding counts</returns>
        public async Task<IEnumerable<UserBadgeDto>> GetBadges(string userId)
        {
            var badgeGifts = await repository.GetBadgeGifts(userId);
            var allBadges = await surveysRepository.GetBadges();
            var badgeCounts = new Dictionary<Badge, int>();
            var userBadgeDtos = new List<UserBadgeDto>();
            foreach (var badge in allBadges)
            {
                badgeCounts.Add(badge, 0);
            }
            foreach (var badgeGift in badgeGifts)
            {
                badgeCounts[badgeGift.Badge] += 1;
            }
            foreach (var item in badgeCounts)
            {
                userBadgeDtos.Add(new() { Count = item.Value, Id = item.Key.Id, Name = item.Key.Name });
            }
            return userBadgeDtos;
        }

        /// <summary>
        /// Gets the full details of a user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>The users full details, or null if the user doesn't exist</returns>
        public async Task<AppUser> GetFullDetails(string userId)
        {
            return await repository.GetUserWithTeamsAndMembers(userId);
        }

        /// <summary>
        /// Returns true if the user is eligible for a course
        /// A user is eligible if they are not already part of a team within that course
        /// </summary>
        /// <param name="user">The ID of the user</param>
        /// <param name="courseId">The ID of the course</param>
        /// <returns>True if the user is eligible, otherwise false</returns>
        private Boolean EligibleForCourse(AppUser user, int courseId)
        {
            return !user.Teams.Any(team => team.CourseId == courseId);
        }

        /// <summary>
        /// Returns True if the users first or last name is contained in the search string
        /// </summary>
        /// <param name="user">The user to be checked</param>
        /// <param name="search">The search string entered to filter users</param>
        /// <returns>True if the users first or last name is in the search string, otherwise false</returns>
        private Boolean FilterByName(AppUser user, string search)
        {
            return user.LastName.Contains(search, StringComparison.CurrentCultureIgnoreCase) || user.FirstName.Contains(search, StringComparison.CurrentCultureIgnoreCase);
        }

    }
}