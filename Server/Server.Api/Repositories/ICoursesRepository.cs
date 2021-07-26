using System;
using System.Collections.Generic;
using Server.Api.Entities;
using System.Threading.Tasks;

namespace Server.Api.Repositories
{
    public interface ICoursesRepository
    {
        Task<Course> Get(int id);
        Task<IEnumerable<Course>> GetAll();
        Task Add(Course course);
        Task Delete(int id);
        Task Update(Course id);
    }
}