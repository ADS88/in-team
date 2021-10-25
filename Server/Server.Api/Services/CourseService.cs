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
    /// <summary>
    /// Business logic for courses
    /// </summary>
    public class CourseService : ICourseService
    {

        private readonly ICoursesRepository repository;

        public CourseService(ICoursesRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Creates a new course with the given name
        /// </summary>
        /// <param name="name">The name of the new course</param>
        /// <returns>The created course</returns>
        public async Task<Course> Create(string name)
        {
            Course course = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repository.Add(course);
            return course;
        }

        /// <summary>
        /// Gets a course by ID
        /// </summary>
        /// <param name="id">The ID of the course to get</param>
        /// <returns>The course with that ID if it exists, or null.</returns>
        public async Task<Course> GetById(int id)
        {
            return await repository.Get(id);
        }

        /// <summary>
        /// Gets all courses within the application
        /// </summary>
        /// <returns>An enumerable of courses</returns>
        public async Task<IEnumerable<Course>> GetAll()
        {
            return await repository.GetAll();
        }

        /// <summary>
        /// Deletes a course with the given ID
        /// </summary>
        /// <param name="id">The ID of the course to delete</param>
        /// <returns>True if the deletion was successful, else false</returns>
        public async Task<Boolean> DeleteCourse(int id)
        {
            var course = await GetById(id);
            if (course is null)
            {
                return false;
            }
            await repository.Delete(course);
            return true;
        }

        /// <summary>
        /// Adds an iteration to a course
        /// </summary>
        /// <param name="name">The name of the iteration</param>
        /// <param name="start">The start date of the iteration</param>
        /// <param name="end">The end date of the iteration</param>
        /// <param name="courseId">The ID of the course to add the iteration to</param>
        /// <returns>The created iteration</returns>
        public async Task<Iteration> AddIteration(string name, DateTimeOffset start, DateTimeOffset end, int courseId)
        {
            Iteration iteration = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow,
                StartDate = start,
                EndDate = end,
                CourseId = courseId
            };
            try
            {
                await repository.AddIteration(iteration);
                return iteration;
            }
            catch (DbUpdateException e)
            {
                return null;
            }

        }

        /// <summary>
        /// Gets a specific iteration of a course
        /// </summary>
        /// <param name="iterationId">The ID of the iteration</param>
        /// <returns>An iteration if it exists, otherwise null</returns>
        public async Task<Iteration> GetIteration(int iterationId)
        {
            return await repository.GetIteration(iterationId);
        }

        /// <summary>
        /// Gets a list of teams that have not yet been assessed in the given iteration
        /// </summary>
        /// <param name="courseId">The ID of the course to get teams from</param>
        /// <param name="iterationId">The iteration to check</param>
        /// <returns>An enumerable of teams yet to be assessed for the given iteration</returns>
        public async Task<IEnumerable<Team>> GetTeamsThatHaveNotBeenGradedInIteration(int courseId, int iterationId)
        {
            var course = await repository.Get(courseId);
            var reviewsForIteration = await repository.GetAchievedStatesFromIteration(iterationId);
            var reviewedTeamIds = reviewsForIteration.Select(review => review.TeamId).ToHashSet();
            return course.Teams.Where(team => !reviewedTeamIds.Contains(team.Id));
        }

        /// <summary>
        /// Gets a list of all iterations
        /// </summary>
        /// <returns>An enumerable of all iterations</returns>
        public async Task<IEnumerable<IterationDto>> GetAllIterations()
        {
            var iterations = await repository.GetAllIterations();
            return iterations.Select(iteration => new IterationDto()
            {
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