using System;
using System.Collections.Generic;
using System.Linq;
using Server.Api.Entities;
using Server.Api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataContext context;
        public UserRepository(IDataContext context){
            this.context = context;
        }

        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await context.AppUsers.ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllWithTeams()
        {
            return await context.AppUsers.Include(user => user.Teams).ToListAsync();
        }

        public async Task<IEnumerable<BadgeGift>> GetBadgeGifts(string userId)
        {
            var badgeGifts = await context.BadgeGifts.Include(bg => bg.User).ToListAsync();
            return badgeGifts.Where(b => b.User.Id == userId);

        }

        public async Task<AppUser> GetUserWithTeams(string userId)
        {
            return await context.AppUsers
                .Include(user => user.Teams)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<AppUser> GetUserWithTeamsAndMembers(string userId)
        {
            return await context.AppUsers
                .Include(user => user.Teams)
                .ThenInclude(team => team.Members)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task UpdateProfileIcon(string userId, string profileIcon){
             var user = await context.AppUsers.FirstOrDefaultAsync(u => u.Id == userId);
             user.ProfileIcon = profileIcon;
             await context.SaveChangesAsync();
        }

    }
}
