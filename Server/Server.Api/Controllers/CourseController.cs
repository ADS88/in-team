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

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await service.GetById(id);
            if (course is null) {
                return NotFound();
            }
            return mapper.Map<CourseDto>(course);
        }

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

        [HttpGet]
        public async Task<IEnumerable<CourseDto>> GetAllCourses(int id)
        {
            var courses = (await service.GetAll()).Select(course => mapper.Map<CourseDto>(course));
            return courses;
        }

        // [HttpGet("{courseId}/pendingiteration/{iterationId}")]
        // public async Task<IEnumerable<CourseDto>> GetTeamsThatHaventBeenGradedInIteration(int courseId, int iterationId)
        // {
        //     var courses = (await service.GetAll()).Select(course => mapper.Map<CourseDto>(course));
        //     return courses;
        // }

        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto courseDto)
        {
            var course = await service.Create(courseDto.Name);
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, mapper.Map<CourseDto>(course));
        }

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

        [HttpGet("iteration/{iterationid}")]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<IterationDto>> AddIteration(int courseId, int iterationId)
        {
            var iteration = await service.GetIteration(iterationId);
            if(iteration is null){
                return NotFound();
            }
            return Ok(iteration);
        }
    }
}
