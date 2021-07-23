using System;
using System.Collections.Generic;
using System.Linq;
using Server.Entities;
using Server.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Repositories
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
            return await context.Courses.FindAsync(id);
        }

        public async Task Add(Course course)
        {
            context.Courses.Add(course);
            await context.SaveChangesAsync();
        }

        public async Task Update(Course user)
        {
            var userToUpdate = await context.Courses.FindAsync(user.Id);
            if(userToUpdate == null){
                throw new NullReferenceException();
            }

            //TODO: Update work here

        }

        public async Task Delete(int id)
        {
            var courseToDelete = await context.Courses.FindAsync(id);
            if(courseToDelete == null){
                throw new NullReferenceException();
            }
            context.Courses.Remove(courseToDelete);;
            await context.SaveChangesAsync();
        }
    }
}
