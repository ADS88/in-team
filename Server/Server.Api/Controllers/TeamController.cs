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
    [Route("api/[controller]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //Gives controller same name as class (route/items)
    public class TeamController : ControllerBase
    {
        private readonly ITeamService service;

        public TeamController(ITeamService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetTeam(int id)
        {
            var team = await service.GetById(id);
            if (team is null)
            {
                return NotFound();
            }
            return team.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateTeam(CreateTeamDto teamDto)
        {
            var team = await service.Create(teamDto.Name, teamDto.Members);
            return CreatedAtAction(nameof(teamDto), new { id = team.Id }, team.AsDto());
        }
    }
}
