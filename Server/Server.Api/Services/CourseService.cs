using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Server.Api.Services
{
    public class CourseService: ICourseService
    {

        private readonly ICoursesRepository repository;

        public CourseService(ICoursesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Course> Create(string Name){
             Course course = new()
            {
                Name = Name,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repository.Add(course);
            return course;
        }

        public async Task<Course> GetById(int id){
            return await repository.Get(id);
        }

         public async Task<IEnumerable<Course>> GetAll(){
            return await repository.GetAll();
        }
    }
}