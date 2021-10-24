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

    /// <summary>
    /// Controller class allowing users to CRUD teams
    /// </summary>
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

        /// <summary>
        /// Endpoint to get all teams in the application
        /// </summary>
        /// <returns>A list of teams</returns>
        [HttpGet]
        [Authorize(Roles = "Lecturer")]
        public async Task<IEnumerable<TeamDto>> GetTeams()
        {
            var teams = (await service.Get()).Select(team => mapper.Map<TeamDto>(team));
            return teams;
        }

        /// <summary>
        /// Endpoint to get a specific team by ID
        /// </summary>
        /// <param name="id">The ID of the team to get</param>
        /// <returns>The team if it is found, otherwise the appropriate HTTP error code</returns>
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


        /// <summary>
        /// Endpoint to delete a team
        /// </summary>
        /// <param name="id">The ID of the team to delete</param>
        /// <returns>200 if the team is successfully deleted, else appropriate HTTP error code</returns>
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

        /// <summary>
        /// Endopoint to create a new team
        /// </summary>
        /// <param name="teamDto">A list of users in the team</param>
        /// <returns>The team if it is created, else appropriate HTTP error code</returns>
        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<TeamDto>> CreateTeam(CreateTeamDto teamDto)
        {
            var team = await service.Create(teamDto.Name, teamDto.CourseId);
            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, mapper.Map<TeamDto>(team));
        }

        /// <summary>
        /// Endpoint to add a student to a team
        /// </summary>
        /// <param name="teamId">The ID of the team</param>
        /// <param name="studentId">The ID of the student to be added</param>
        /// <returns>HTTP 200 if the student is added, else appropriate HTTP error code</returns>
        [HttpPost("{teamId}/addstudent/{studentId}")]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<TeamDto>> CreateTeam(int teamId, string studentId)
        {
            await service.AddMember(teamId, studentId);
            return Ok();
        }


        /// <summary>
        /// Endpoint to grade a team for a specific iteration
        /// </summary>
        /// <param name="dto">The states that the team achieved, and the points to be awarded</param>
        /// <param name="teamId">The ID of the graded team</param>
        /// <param name="iterationId">The ID of the iteration the team is being graded for</param>
        /// <returns></returns>
        [HttpPost("{teamId}/achievestates/{iterationId}")]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<TeamDto>> AchieveStates(AchievedStateDto dto, int teamId, int iterationId)
        {
            await service.AchieveStates(dto, teamId, iterationId);
            return Ok();
        }

        /// <summary>
        /// Endpoint to get the current states that the team is in for each alpha
        /// </summary>
        /// <param name="teamId">The ID of the team to get states for</param>
        /// <returns>A list of states that the team has achieved</returns>
        [HttpGet("{teamId}/currentstates")]
        public async Task<ActionResult<TeamDto>> GetCurrentStates(int teamId)
        {
            var currentStates = await service.GetTeamsCurrentStates(teamId);
            return Ok(currentStates);
        }


        /// <summary>
        /// Gets the averages of survey results for a specific team and iteration
        /// </summary>
        /// <param name="teamId">The ID of the team to be graded</param>
        /// <param name="iterationId">The iteration the results come from</param>
        /// <returns>A summary of the teams survey answers from that iteration</returns>
        [HttpGet("{teamId}/surveyresults/{iterationId}")]
        public async Task<ActionResult<TeamDto>> GetSurveyResults(int teamId, int iterationId)
        {
            var surveyAnswerSummaries = await service.GetTeamsSurveyAnswerSummaries(teamId, iterationId);
            return Ok(surveyAnswerSummaries);
        }
    }
}
