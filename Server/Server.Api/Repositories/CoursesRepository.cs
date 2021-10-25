using System.Linq;
using System.Collections.Generic;
using Server.Api.Entities;
using Server.Api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Repositories
{
    /// <summary>
    /// Repository class for course database operations
    /// </summary>
    public class CoursesRepository : ICoursesRepository
    {
        private readonly IDataContext context;
        public CoursesRepository(IDataContext context){
            this.context = context;
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await context.Courses.Include(course => course.Teams).ToListAsync();
        }

        /// <summary>
        /// Gets a course from the database with the specified ID
        /// </summary>
        /// <param name="id">The ID of the course to get</param>
        /// <returns>A course, or null</returns>
        public async Task<Course> Get(int id)
        {
            return await context.Courses.
            Include(course => course.Teams)
            .Include(course => course.Iterations)
            .FirstOrDefaultAsync(course => course.Id == id);
        }

        /// <summary>
        /// Adds a course to the database
        /// </summary>
        /// <param name="course">The course to add</param>
        /// <returns></returns>
        public async Task Add(Course course)
        {
            context.Courses.Add(course);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a course from the database
        /// </summary>
        /// <param name="course">The course to delete</param>
        /// <returns></returns>
        public async Task Delete(Course course)
        {
            context.Courses.Remove(course);
            await context.SaveChangesAsync();
        }


        /// <summary>
        /// Adds an iteration to a course within the database
        /// </summary>
        /// <param name="iteration">The iteration to add</param>
        /// <returns></returns>
        public async Task AddIteration(Iteration iteration)
        {
            context.Iterations.Add(iteration);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a specific iteration from the database
        /// </summary>
        /// <param name="iterationId">The ID of the iteration to get</param>
        /// <returns>The first iteration with that ID, or null if it doesn't exist</returns>
        public async Task<Iteration> GetIteration(int iterationId)
        {
            return await context.Iterations.FirstOrDefaultAsync(i => i.Id == iterationId);
        }

        /// <summary>
        /// Gets the states a team has achieved from an iteration from the database
        /// </summary>
        /// <param name="iterationId">The ID of the iteration</param>
        /// <returns>A collection of states achieved for that iteration</returns>
        public async Task<ICollection<AchievedState>> GetAchievedStatesFromIteration(int iterationId){
            var achievedStates = await context.AchievedStates.ToListAsync();
            return achievedStates.Where(state => state.IterationId == iterationId ).ToList();
        }

        /// <summary>
        /// Gets all iterations in the database, including their linked courses
        /// </summary>
        /// <returns>All iterations within the database</returns>
        public async Task<ICollection<Iteration>> GetAllIterations(){
            return await context.Iterations
                        .Include(iteration => iteration.Course)
                        .ToListAsync();
        }
    }
}
