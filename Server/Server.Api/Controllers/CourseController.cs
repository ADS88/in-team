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
using Server.Api.Services;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //Gives controller same name as class (route/items)
    public class CourseController : ControllerBase
    {
        private readonly ICourseService service;

        public CourseController(ICourseService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await service.GetById(id);
            if (course is null)
            {
                return NotFound();
            }
            return course.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto courseDto)
        {
            var course = await service.Create(courseDto.Name);
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course.AsDto());
        }
    }
}
