using System;
using Server.Api.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Server.Api.Dtos;

namespace Server.Api.Services

{
    /// <summary>
    /// An interface for business logic related to courses
    /// </summary>
    public interface ICourseService
    {
        Task<Course> GetById(int id);
        Task<IEnumerable<Course>> GetAll();
        Task<Course> Create(string name);
        Task<Iteration> AddIteration(string name, DateTimeOffset start, DateTimeOffset end, int courseId);
        Task<Boolean> DeleteCourse(int id);
        Task<Iteration> GetIteration(int id);
        Task<IEnumerable<Team>> GetTeamsThatHaveNotBeenGradedInIteration(int courseId, int iterationId);
        Task<IEnumerable<IterationDto>> GetAllIterations();
    }
}