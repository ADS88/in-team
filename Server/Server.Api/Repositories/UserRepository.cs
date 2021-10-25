using System.Collections.Generic;
using System.Linq;
using Server.Api.Entities;
using Server.Api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Repositories
{
    /// <summary>
    /// Repository class for user database operations
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IDataContext context;
        public UserRepository(IDataContext context){
            this.context = context;
        }

        /// <summary>
        /// Gets all users from the database
        /// </summary>
        /// <returns>An enumerable of users</returns>
        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await context.AppUsers.ToListAsync();
        }

        /// <summary>
        /// Gets all users from the database, alongside the teams they are part of
        /// </summary>
        /// <returns>An enumerable of users, with their teams also loaded</returns>
        public async Task<IEnumerable<AppUser>> GetAllWithTeams()
        {
            return await context.AppUsers.Include(user => user.Teams).ToListAsync();
        }

        /// <summary>
        /// Gets the badges that a user has been gifted, and their quantities
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>An enumerable of badges and their quantities</returns>
        public async Task<IEnumerable<BadgeGift>> GetBadgeGifts(string userId)
        {
            var badgeGifts = await context.BadgeGifts.Include(bg => bg.User).ToListAsync();
            return badgeGifts.Where(b => b.User.Id == userId);

        }

        /// <summary>
        /// Gets a specific user from the database, also including their teams data
        /// </summary>
        /// <param name="userId">The ID of the user to get</param>
        /// <returns>A user, including the teams they are part of</returns>
        public async Task<AppUser> GetUserWithTeams(string userId)
        {
            return await context.AppUsers
                .Include(user => user.Teams)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        /// <summary>
        /// Gets a specific user from the database, also including their teams data
        /// And data for each member of their team
        /// </summary>
        /// <param name="userId">The ID of the user to get data for</param>
        /// <returns>A user including the teams they are part of, and team member data</returns>
        public async Task<AppUser> GetUserWithTeamsAndMembers(string userId)
        {
            return await context.AppUsers
                .Include(user => user.Teams)
                .ThenInclude(team => team.Members)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        /// <summary>
        /// Updates a users profile icon in the database
        /// </summary>
        /// <param name="userId">The ID of the user to update</param>
        /// <param name="profileIcon">The new profile icon</param>
        /// <returns></returns>
        public async Task UpdateProfileIcon(string userId, string profileIcon){
             var user = await context.AppUsers.FirstOrDefaultAsync(u => u.Id == userId);
             user.ProfileIcon = profileIcon;
             await context.SaveChangesAsync();
        }

    }
}
