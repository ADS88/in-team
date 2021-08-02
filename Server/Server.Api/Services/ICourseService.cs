using Server.Api.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server.Api.Services

{
    public interface ICourseService
    {
        Task<Course> GetById(int id);

        Task<IEnumerable<Course>> GetAll();

        Task<Course> Create(string name);
    }
}