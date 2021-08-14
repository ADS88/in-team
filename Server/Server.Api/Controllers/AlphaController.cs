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
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet("{alphaId}/states")]
        public async Task<IEnumerable<StateDto>> GetStates(int alphaId)
        {
            var states = (await service.GetStates(alphaId)).Select(state => mapper.Map<StateDto>(state));
            return states;
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateAlpha(CreateAlphaDto alphaDto)
        {
            var alpha = await service.CreateAlpha(alphaDto.Name);
            return CreatedAtAction(nameof(GetAlphas), new { id = alpha.Id }, mapper.Map<AlphaDto>(alpha));
        }

        [HttpPost("{alphaId}/question")]
        public async Task<ActionResult<CourseDto>> AddState(CreateStateDto stateDto, int alphaId)
        {
            var state = await service.AddState(stateDto.Name, alphaId);
            return CreatedAtAction(nameof(GetStates), new { id = state.Id }, mapper.Map<StateDto>(state));
        }

        [HttpPost("{stateId}/state")]
        public async Task<ActionResult<CourseDto>> AddQuestion(CreateQuestionDto questionDto, int stateId)
        {
            var alpha = await service.AddQuestion(questionDto.Content, stateId);
            return CreatedAtAction(nameof(GetAlphas), new { id = alpha.Id }, mapper.Map<StateDto>(alpha));
        }
    }
}
