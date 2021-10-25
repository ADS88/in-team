using System.Collections.Generic;
using Server.Api.Entities;
using System.Threading.Tasks;

namespace Server.Api.Repositories
{
    /// <summary>
    /// Interface for user database operations
    /// </summary>
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAll();
        Task<IEnumerable<AppUser>> GetAllWithTeams();
        Task<AppUser> GetUserWithTeams(string userId);
        Task<AppUser> GetUserWithTeamsAndMembers(string userId);
        Task UpdateProfileIcon(string userId, string newIcon);

        Task<IEnumerable<BadgeGift>> GetBadgeGifts(string userId);

    }
}