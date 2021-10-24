using System.Linq;
using Server.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Server.Api.Services;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Server.Api.Entities;
using System;

namespace Server.Api.Controllers
{
    /// <summary>
    /// Controller used to CRUD students
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentController : ControllerBase
    {

        private readonly IUserService service;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public StudentController(IUserService service, IMapper mapper, UserManager<AppUser> userManager)
        {
            this.service = service;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        /// <summary>
        /// Endpoint allowing lecturers to get all students in the app
        /// </summary>
        /// <returns>A list of all students in the application</returns>
        [HttpGet]
        [Authorize(Roles = "Lecturer")]
        public async Task<IEnumerable<UserDto>> GetAllStudents()
        {
            var students = (await service.GetAll()).Select(student => mapper.Map<UserDto>(student));
            return students;
        }

        /// <summary>
        /// Endpoint to get a student by ID
        /// </summary>
        /// <param name="id">the ID of the student</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FullUserDto>> GetCurrentStudent(string id)
        {
            var student = await service.GetFullDetails(id);
            if(student == null){
                return NotFound();
            }
            return mapper.Map<FullUserDto>(student);
        }

        /// <summary>
        /// Endpoint to get a students badges
        /// </summary>
        /// <param name="id">The ID of the student</param>
        /// <returns>A list of the students badges</returns>
        [HttpGet("{id}/badges")]
        public async Task<ActionResult<IEnumerable<UserBadgeDto>>> GetStudentBadges(string id)
        {
            var badges = await service.GetBadges(id);
            if(badges == null){
                return NotFound();
            }
            return Ok(badges);
        }

        /// <summary>
        /// Endpoint allowing a user to change their profile icon
        /// </summary>
        /// <param name="dto">The new profile icon that they want</param>
        /// <param name="id">The ID of the user who's icon to change</param>
        /// <returns>HTTP 200 if change is successful, else appropriate HTTP error code</returns>
        [HttpPatch("{id}/profile-icon")]
        public async Task<ActionResult> UpdateProfileIcon(UpdateProfileIconDto dto, string id)
        {
            var possibleProfileIcons =  new string[]{"angler", "hamburger", "dog","lion", "octopus", "samurai", "sunbeams", "walrus","muscles", "sushi", "kiwi",
            "mantaray", "bird", "squid", "inspiration", "dinosaur", "winner", "backstab", "buffalo", "snowman", "spectre", "spacesuit", "spider", "totem", "monkey","fisherman", "pumpkin", "robot"};
            
            if(!possibleProfileIcons.Contains(dto.ProfileIcon)){
                return BadRequest();
            }
            var userId = User.Claims.Where(x => x.Type == "Id").FirstOrDefault()?.Value;
            if(userId != id){
                return Unauthorized();
            }

            var student = await userManager.FindByIdAsync(userId);
            if(student == null){
                return NotFound();
            }
            await service.UpdateProfileIcon(userId, dto.ProfileIcon);
            return Ok();
        }

        /// <summary>
        /// Endpoint to get all students eligible for a course
        /// </summary>
        /// <param name="teamId">The ID of the team we are trying to add the user to</param>
        /// <param name="search">The users first or last name</param>
        /// <returns></returns>
        [HttpGet("course/{teamId}")]
        public async Task<IEnumerable<UserDto>> GetStudentsEligibleForCourse(int teamId, [FromQuery]string search)
        {
            var students = (await service.GetEligibleForCourse(teamId, search)).Select(student => mapper.Map<UserDto>(student));
            return students;
        }

    }
}
