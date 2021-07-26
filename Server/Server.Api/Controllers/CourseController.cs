using System;
using System.Linq;
using Server.Api.Dtos;
using Server.Api.Entities;
using Server.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //Gives controller same name as class (route/items)
    public class CourseController : ControllerBase
    {
        private readonly ICoursesRepository repository;

        public CourseController(ICoursesRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await repository.Get(id);
            if (course is null)
            {
                return NotFound();
            }
            return course.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateUser(CreateCourseDto courseDto)
        {
            Course course = new()
            {
                Id = 1,
                Name = courseDto.Name,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repository.Add(course);
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course.AsDto());
        }
    }
}
