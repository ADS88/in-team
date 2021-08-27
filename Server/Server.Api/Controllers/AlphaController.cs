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

        [HttpGet]
        public async Task<IEnumerable<AlphaDto>> GetAlphas()
        {
            var alphas = (await service.GetAlphas()).Select(alpha => mapper.Map<AlphaDto>(alpha));
            return alphas;
        }

        [HttpGet("{id}")]
        public async Task<AlphaDto> GetAlpha(int id)
        {
            var alpha = await service.GetAlpha(id);
            return mapper.Map<AlphaDto>(alpha);
        }

        [HttpGet("state/{id}")]
        public async Task<StateDto> GetState(int id)
        {
            var state = await service.GetState(id);
            return mapper.Map<StateDto>(state);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateAlpha(CreateAlphaDto alphaDto)
        {
            var alpha = await service.CreateAlpha(alphaDto.Name);
            return CreatedAtAction(nameof(GetAlpha), new { id = alpha.Id }, mapper.Map<AlphaDto>(alpha));
        }

        [HttpPost("{alphaId}/state")]
        public async Task<ActionResult<CourseDto>> AddState(CreateStateDto stateDto, int alphaId)
        {
            var state = await service.AddState(stateDto.Name, alphaId);
            return CreatedAtAction(nameof(GetAlpha), new { id = state.Id }, mapper.Map<StateDto>(state));
        }

        [HttpPost("state/{stateId}/question")]
        public async Task<ActionResult<CourseDto>> AddQuestion(CreateQuestionDto questionDto, int stateId)
        {
            var question = await service.AddQuestion(questionDto.Content, stateId);
            return CreatedAtAction(nameof(GetAlpha), new { id = question.Id }, mapper.Map<QuestionDto>(question));
        }
    }
}
