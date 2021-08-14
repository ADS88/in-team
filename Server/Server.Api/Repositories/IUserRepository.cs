using System.Collections.Generic;
using Server.Api.Entities;
using System.Threading.Tasks;

namespace Server.Api.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAll();
    }
}