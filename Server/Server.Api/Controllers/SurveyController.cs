using Microsoft.AspNetCore.Mvc;
using Server.Api.Services;
using AutoMapper;
using Server.Api.Dtos;
using System.Threading.Tasks;

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
        public async Task<ActionResult<SurveyDto>> GetSurvey(){
            return Ok();
        } 

        [HttpPost]
        public async Task<ActionResult<SurveyDto>> CreateSurvey(CreateSurveyDto dto)
        {
            var survey = await service.Create(dto.Name, dto.StateIds, dto.TeamIds, dto.OpeningDate, dto.ClosingDate);
            return CreatedAtAction(nameof(GetSurvey), new { id = survey.Id }, mapper.Map<SurveyDto>(survey));
        }
    }
}