using Microsoft.AspNetCore.Mvc;
using Server.Api.Services;
using AutoMapper;
using Server.Api.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<SurveyDto>> CreateSurvey(CreateSurveyDto dto)
        {
            var survey = await service.Create(dto.Name, dto.StateIds, dto.TeamIds, dto.OpeningDate, dto.ClosingDate, dto.IterationId);
            return CreatedAtAction(nameof(GetSurveys), new { id = survey.Id }, mapper.Map<SurveyDto>(survey));
        }

        [HttpGet("{id}")]
        public async Task<SurveyQuestionsDto> GetQuestions(int id){
            var survey = await service.Get(id);
            return mapper.Map<SurveyQuestionsDto>(survey);
        }

        [HttpGet("{surveyId}/members")]
        public async Task<IEnumerable<UserDto>> GetMembers(int surveyId){
            var userId = User.Claims.Where(x => x.Type == "Id").FirstOrDefault()?.Value;
            var users = await service.FindTeamMembersFromSurvey(surveyId, userId);
            return users.Select(user => mapper.Map<UserDto>(user));
        }

        [HttpGet("badges")]
        public async Task<IEnumerable<BadgeDto>> GetBadges(){
            var badges = await service.GetBadges();
            return badges.Select(badge => mapper.Map<BadgeDto>(badge));
        }

        [HttpGet("pending")]
        public async Task<IEnumerable<SurveyDto>> GetSurveysStudentNeedsToComplete(){
            var userId = User.Claims.Where(x => x.Type == "Id").FirstOrDefault()?.Value;
            var surveys = await service.GetSurveysStudentNeedsToComplete(userId);
            return surveys.Select(survey => mapper.Map<SurveyDto>(survey));
        }

        [HttpPost("{id}/answer")]
        public async Task<ActionResult<SurveyDto>> AnswerSurvey(AnswerSurveyDto dto, int id)
        {
            var userId = User.Claims.Where(x => x.Type == "Id").FirstOrDefault()?.Value;
            var surveyAttempt = await service.AnswerSurvey(dto, id, userId);
            return CreatedAtAction(nameof(GetSurveys), new { id = surveyAttempt.Id }, mapper.Map<SurveyAttemptDto>(surveyAttempt));
        }
    }
}