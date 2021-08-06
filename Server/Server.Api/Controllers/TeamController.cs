using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Server.Api.Services;
using AutoMapper;
using Server.Api.Dtos;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //Gives controller same name as class (route/items)
    public class TeamController : ControllerBase
    {
        private readonly ITeamService service;

        private readonly IMapper mapper;

        public TeamController(ITeamService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetTeam(int id)
        {
            var team = await service.GetById(id);
            if (team is null)
            {
                return NotFound();
            }
            return mapper.Map<TeamDto>(team);
        }

        [HttpPost]
        public async Task<ActionResult<TeamDto>> CreateTeam(CreateTeamDto teamDto)
        {
            var team = await service.Create(teamDto.Name, teamDto.CourseId);
            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, mapper.Map<TeamDto>(team));
        }
    }
}
