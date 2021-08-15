using Server.Api.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server.Api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetAll();
        Task<IEnumerable<AppUser>> GetEligibleForCourse(int courseId, string searchString);
    }
}