using System;
using System.Collections.Generic;
using System.Linq;
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
            return await context.Courses.ToListAsync();
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

        public async Task Update(Course course)
        {
            var courseToUpdate = await context.Courses.FindAsync(course.Id);
            if(courseToUpdate == null){
                throw new NullReferenceException();
            }

            //TODO: Update work here

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
    }
}
