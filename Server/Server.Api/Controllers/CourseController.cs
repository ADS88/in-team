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
            return mapper.Map<CourseDto>(course);
        }

        [HttpDelete("{id}")]
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

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto courseDto)
        {
            var course = await service.Create(courseDto.Name);
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, mapper.Map<CourseDto>(course));
        }
    }
}
