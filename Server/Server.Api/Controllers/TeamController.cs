using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Server.Api.Services;
using AutoMapper;
using Server.Api.Dtos;
using System.Linq;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService service;

        private readonly IMapper mapper;

        public TeamController(ITeamService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Lecturer")]
        public async Task<IEnumerable<TeamDto>> GetTeams()
        {
            var teams = (await service.Get()).Select(team => mapper.Map<TeamDto>(team));
            return teams;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<TeamDto>> GetTeam(int id)
        {
            var team = await service.GetById(id);
            if (team is null)
            {
                return NotFound();
            }
            return mapper.Map<TeamDto>(team);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            var deleted = await service.DeleteTeam(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<TeamDto>> CreateTeam(CreateTeamDto teamDto)
        {
            var team = await service.Create(teamDto.Name, teamDto.CourseId);
            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, mapper.Map<TeamDto>(team));
        }

        [HttpPost("{teamId}/addstudent/{studentId}")]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<TeamDto>> CreateTeam(int teamId, string studentId)
        {
            await service.AddMember(teamId, studentId);
            return Ok();
        }

        [HttpPost("{teamId}/achievestates/{iterationId}")]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<TeamDto>> AchieveStates(AchievedStateDto dto, int teamId, int iterationId)
        {
            await service.AchieveStates(dto, teamId, iterationId);
            return Ok();
        }

        [HttpGet("{teamId}/currentstates")]
        public async Task<ActionResult<TeamDto>> GetCurrentStates(int teamId)
        {
            var currentStates = await service.GetTeamsCurrentStates(teamId);
            return Ok(currentStates);
        }
    }
}
