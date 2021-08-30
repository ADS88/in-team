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

        [HttpGet]
        [Authorize(Roles = "Lecturer")]
        public async Task<IEnumerable<UserDto>> GetAllStudents()
        {
            var students = (await service.GetAll()).Select(student => mapper.Map<UserDto>(student));
            return students;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FullUserDto>> GetCurrentStudent(string id)
        {
            var student = await service.GetFullDetails(id);
            if(student == null){
                return NotFound();
            }
            return mapper.Map<FullUserDto>(student);
        }

        [HttpGet("{id}/badges")]
        public async Task<ActionResult<IEnumerable<UserBadgeDto>>> GetStudentBadges(string id)
        {
            var badges = await service.GetBadges(id);
            if(badges == null){
                return NotFound();
            }
            return Ok(badges);
        }

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

        [HttpGet("course/{teamId}")]
        public async Task<IEnumerable<UserDto>> GetStudentsEligibleForCourse(int teamId, [FromQuery]string search)
        {
            var students = (await service.GetEligibleForCourse(teamId, search)).Select(student => mapper.Map<UserDto>(student));
            return students;
        }

    }
}
