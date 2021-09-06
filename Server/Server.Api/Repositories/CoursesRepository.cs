using System.Linq;
using System.Collections.Generic;
using Server.Api.Entities;
using Server.Api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Repositories
{
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

        public async Task<Course> Get(int id)
        {
            return await context.Courses.
            Include(course => course.Teams)
            .Include(course => course.Iterations)
            .FirstOrDefaultAsync(course => course.Id == id);
        }

        public async Task Add(Course course)
        {
            context.Courses.Add(course);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Course course)
        {
            context.Courses.Remove(course);
            await context.SaveChangesAsync();
        }

        public async Task AddIteration(Iteration iteration)
        {
            context.Iterations.Add(iteration);
            await context.SaveChangesAsync();
        }

        public async Task<Iteration> GetIteration(int iterationId)
        {
            return await context.Iterations.FirstOrDefaultAsync(i => i.Id == iterationId);
        }

        public async Task<ICollection<AchievedState>> GetAchievedStatesFromIteration(int iterationId){
            var achievedStates = await context.AchievedStates.ToListAsync();
            return achievedStates.Where(state => state.IterationId == iterationId ).ToList();
        }
        public async Task<ICollection<Iteration>> GetAllIterations(){
            return await context.Iterations
                        .Include(iteration => iteration.Course)
                        .ToListAsync();
        }
    }
}
