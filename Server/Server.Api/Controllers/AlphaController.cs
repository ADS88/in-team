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
    /// <summary>
    /// Endpoints allowing lecturers to add and remove alphas and states
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Lecturer")]
    public class AlphaController : ControllerBase
    {
        private readonly IAlphaService service;

        private readonly IMapper mapper;

        public AlphaController(IAlphaService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets all Alphas within the system
        /// </summary>
        /// <returns>A list of all alphas in the system</returns>
        [HttpGet]
        public async Task<IEnumerable<AlphaDto>> GetAlphas()
        {
            var alphas = (await service.GetAlphas()).Select(alpha => mapper.Map<AlphaDto>(alpha));
            return alphas;
        }

        /// <summary>
        /// Gets a specific alpha by ID
        /// </summary>
        /// <param name="id">The ID of the alpha to get</param>
        /// <returns>A DTO with the alpha's details</returns>
        [HttpGet("{id}")]
        public async Task<AlphaDto> GetAlpha(int id)
        {
            var alpha = await service.GetAlpha(id);
            return mapper.Map<AlphaDto>(alpha);
        }

        /// <summary>
        /// Gets a specific state by ID
        /// </summary>
        /// <param name="id">The ID of the state to get</param>
        /// <returns>A DTO with the states's details</returns>
        [HttpGet("state/{id}")]
        public async Task<StateDto> GetState(int id)
        {
            var state = await service.GetState(id);
            return mapper.Map<StateDto>(state);
        }

        /// <summary>
        /// Creates a new alpha
        /// </summary>
        /// <param name="alphaDto">A DTO containing the new Alpha's name</param>
        /// <returns>An action showing whether creation was successful</returns>
        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateAlpha(CreateAlphaDto alphaDto)
        {
            var alpha = await service.CreateAlpha(alphaDto.Name);
            return CreatedAtAction(nameof(GetAlpha), new { id = alpha.Id }, mapper.Map<AlphaDto>(alpha));
        }

        /// <summary>
        /// Creates a new state
        /// </summary>
        /// <param name="stateDto">A DTO containing the new States's name</param>
        /// <param name="alphaId">The ID of the Alpha the new state is linked to</param>
        /// <returns>An action showing whether creation was successful</returns>
        [HttpPost("{alphaId}/state")]
        public async Task<ActionResult<CourseDto>> AddState(CreateStateDto stateDto, int alphaId)
        {
            var state = await service.AddState(stateDto.Name, alphaId);
            return CreatedAtAction(nameof(GetAlpha), new { id = state.Id }, mapper.Map<StateDto>(state));
        }

        /// <summary>
        /// Adds a question to an existing state
        /// </summary>
        /// <param name="questionDto">A DTO containing the new questions content</param>
        /// <param name="stateId">The ID of the state that the question relates to</param>
        /// <returns>An action showing whether creation was successful</returns>
        [HttpPost("state/{stateId}/question")]
        public async Task<ActionResult<CourseDto>> AddQuestion(CreateQuestionDto questionDto, int stateId)
        {
            var question = await service.AddQuestion(questionDto.Content, stateId);
            return CreatedAtAction(nameof(GetAlpha), new { id = question.Id }, mapper.Map<QuestionDto>(question));
        }
    }
}
