using System.Collections.Generic;
using Server.Api.Entities;
using System.Threading.Tasks;

namespace Server.Api.Repositories
{
    /// <summary>
    /// Interface for course database operations
    /// </summary>
    public interface ICoursesRepository
    {
        Task<Course> Get(int id);
        Task<IEnumerable<Course>> GetAll();
        Task Add(Course course);
        Task Delete(Course course);
        Task AddIteration(Iteration iteration);
        Task<Iteration> GetIteration(int iterationId);
        Task<ICollection<AchievedState>> GetAchievedStatesFromIteration(int iterationId);
        Task<ICollection<Iteration>> GetAllIterations();
    }
}