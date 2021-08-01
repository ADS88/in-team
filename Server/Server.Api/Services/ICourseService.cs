using Server.Api.Entities;
using Server.Api.Dtos;
using System.Threading.Tasks;

namespace Server.Api.Services

{
    public interface ICourseService
    {
        Task<Course> GetById(int id);

        Task<Course> Create(string name);
    }
}