using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Services
{
    public class CourseService: ICourseService
    {

        private readonly ICoursesRepository repository;

        public CourseService(ICoursesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Course> Create(string name){
            Course course = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repository.Add(course);
            return course;
        }

        public async Task<Course> GetById(int id){
            var course =  await repository.Get(id);
            if(course is null) throw new NullReferenceException();
            return course;
        }

         public async Task<IEnumerable<Course>> GetAll(){
            return await repository.GetAll();
        }

        public async Task<Boolean> DeleteCourse(int id){
            var course = await GetById(id);
            if(course is null){
                return false;
            }
            await repository.Delete(course);
            return true;
        }

        public async Task<Iteration> AddIteration(string name, DateTimeOffset start, DateTimeOffset end, int courseId){
            Iteration iteration = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow,
                StartDate = start,
                EndDate = end,
                CourseId = courseId
            };
            try {
            await repository.AddIteration(iteration);
            return iteration;
            } catch (DbUpdateException e){
                return null;
            }
           
        }
    }
}