using Microsoft.AspNetCore.Mvc;
using Server.Api.Services;
using AutoMapper;
using Server.Api.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService service;

        private readonly IMapper mapper;

        public SurveyController(ISurveyService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SurveyDto>> GetSurveys(){
            var surveys = (await service.GetAll()).Select(survey => mapper.Map<SurveyDto>(survey));
            return surveys;
        } 

        [HttpPost]
        public async Task<ActionResult<SurveyDto>> CreateSurvey(CreateSurveyDto dto)
        {
            var survey = await service.Create(dto.Name, dto.StateIds, dto.TeamIds, dto.OpeningDate, dto.ClosingDate);
            return CreatedAtAction(nameof(GetSurveys), new { id = survey.Id }, mapper.Map<SurveyDto>(survey));
        }

        [HttpGet("{id}")]
        public async Task<SurveyQuestionsDto> GetQuestions(int id){
            var survey = await service.Get(id);
            return mapper.Map<SurveyQuestionsDto>(survey);
        }
    }
}