using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Server.Api.Dtos;

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
            return await repository.Get(id);
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

        public async Task<Iteration> GetIteration(int iterationId){
            return await repository.GetIteration(iterationId);
        }

        public async Task<IEnumerable<Team>> GetTeamsThatHaveNotBeenGradedInIteration(int courseId, int iterationId){
            var course = await repository.Get(courseId);
            var reviewsForIteration = await repository.GetAchievedStatesFromIteration(iterationId);
            var reviewedTeamIds = reviewsForIteration.Select(review => review.TeamId).ToHashSet();
            return course.Teams.Where(team => !reviewedTeamIds.Contains(team.Id));
        }

        public async Task<IEnumerable<IterationDto>> GetAllIterations(){
            var iterations = await repository.GetAllIterations();
            return iterations.Select(iteration => new IterationDto(){
                Id = iteration.Id,
                Name = iteration.Name,
                CreatedDate = iteration.CreatedDate,
                StartDate = iteration.StartDate,
                EndDate = iteration.EndDate,
                CourseId = iteration.CourseId,
                CourseName = iteration.Course.Name
            });
        }
    }
}