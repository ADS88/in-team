using System;
using System.Linq;
using Server.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Server.Api.Services;
using System.Collections.Generic;
using AutoMapper;

namespace Server.Api.Controllers
{

    /// <summary>
    /// Allows the user to create and delete courses and iterations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService service;

        private readonly IMapper mapper;

        public CourseController(ICourseService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }


        /// <summary>
        /// Endpoint to get a specific course by it's ID
        /// </summary>
        /// <param name="id">The id of the course</param>
        /// <returns>The course or appropriate HTTP error code</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await service.GetById(id);
            if (course is null) {
                return NotFound();
            }
            return mapper.Map<CourseDto>(course);
        }

        /// <summary>
        /// Endpoint to delete a course
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200 if deletion successful, else appropriate HTTP error code</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            var deleted = await service.DeleteCourse(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Endpoint to get all courses within the application
        /// </summary>
        /// <returns>A list of all courses within the application</returns>
        [HttpGet]
        public async Task<IEnumerable<CourseDto>> GetAllCourses()
        {
            var courses = (await service.GetAll()).Select(course => mapper.Map<CourseDto>(course));
            return courses;
        }


        /// <summary>
        /// Endpoint to gets all teams from a course that have not yet been graded for a specific iteration
        /// </summary>
        /// <param name="courseId">The ID of the course to get teams from</param>
        /// <param name="iterationId">The ID of the iteration to be checked</param>
        /// <returns>A list of Teams that have yet to be graded for that iteration</returns>
        [HttpGet("{courseId}/pendingiteration/{iterationId}")]
        public async Task<IEnumerable<TeamDto>> GetTeamsThatHaventBeenGradedInIteration(int courseId, int iterationId)
        {
            var teams = await service.GetTeamsThatHaveNotBeenGradedInIteration(courseId, iterationId);
            return teams.Select(team => mapper.Map<TeamDto>(team));
        }

        /// <summary>
        /// Endpoint to create a course
        /// </summary>
        /// <param name="courseDto">DTO containing the name of the course</param>
        /// <returns>201 response if successful, else appropriate HTTP error code</returns>
        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto courseDto)
        {
            var course = await service.Create(courseDto.Name);
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, mapper.Map<CourseDto>(course));
        }

        /// <summary>
        /// Endpoint to add an interation (e.g sprint in scrum) to a course
        /// </summary>
        /// <param name="iterationDto">A DTO containing details of the iteration</param>
        /// <param name="id">The ID of the course to add the iteration to</param>
        /// <returns>201 response if successful, else appropriate HTTP error code</returns>
        [HttpPost("{id}/iteration")]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<IterationDto>> AddIteration(CreateIterationDto iterationDto, int id)
        {
            var iteration = await service.AddIteration(iterationDto.Name, iterationDto.StartDate, iterationDto.EndDate, id);
            if(iteration is null){
                return UnprocessableEntity();
            }
            return CreatedAtAction(nameof(GetCourse), new { id = iteration.Id }, mapper.Map<IterationDto>(iteration));
        }

        /// <summary>
        /// Endpoint to get an iteration by ID
        /// </summary>
        /// <param name="iterationId">The ID of the iteration to get</param>
        /// <returns>The iteration if it exists, else HTTP 404</returns>
        [HttpGet("iteration/{iterationid}")]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<IterationDto>> AddIteration(int iterationId)
        {
            var iteration = await service.GetIteration(iterationId);
            if(iteration is null){
                return NotFound();
            }
            return Ok(iteration);
        }

        /// <summary>
        /// Endpoint to get all iterations in the application
        /// </summary>
        /// <returns>All iterations in the application</returns>
        [HttpGet("iteration")]
        [Authorize(Roles = "Lecturer")]
         public async Task<IEnumerable<IterationDto>> GetAllIterations()
        {
            return await service.GetAllIterations();     
        }
    }
}
