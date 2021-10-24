using Microsoft.AspNetCore.Mvc;
using Server.Api.Services;
using AutoMapper;
using Server.Api.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Server.Api.Controllers
{
    /// <summary>
    /// Controller class allowing users to CRUD surveys
    /// </summary>
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

        /// <summary>
        /// Endpoint to get all surveys within the application
        /// </summary>
        /// <returns>A list of all the surveys within the application</returns>
        [HttpGet]
        public async Task<IEnumerable<SurveyDto>> GetSurveys(){
            var surveys = (await service.GetAll()).Select(survey => mapper.Map<SurveyDto>(survey));
            return surveys;
        } 

        /// <summary>
        /// Endpoint to create a new survey
        /// </summary>
        /// <param name="dto">A DTO containing information about the new survey</param>
        /// <returns>HTTP 200 if creation is successful, else appropriate HTTP error code</returns>
        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public async Task<ActionResult<SurveyDto>> CreateSurvey(CreateSurveyDto dto)
        {
            var survey = await service.Create(dto.Name, dto.StateIds, dto.TeamIds, dto.OpeningDate, dto.ClosingDate, dto.IterationId);
            return CreatedAtAction(nameof(GetSurveys), new { id = survey.Id }, mapper.Map<SurveyDto>(survey));
        }

        /// <summary>
        /// Endpoint to get all questions in a survey
        /// </summary>
        /// <param name="id">The ID of the survey</param>
        /// <returns>A list of questions within a survey</returns>
        [HttpGet("{id}")]
        public async Task<SurveyQuestionsDto> GetQuestions(int id){
            var survey = await service.Get(id);
            return mapper.Map<SurveyQuestionsDto>(survey);
        }

        /// <summary>
        /// Endpoint to get all team members of a survey
        /// </summary>
        /// <param name="surveyId">The ID of the survey to get members from</param>
        /// <returns>A list of users who are part of that survey</returns>
        [HttpGet("{surveyId}/members")]
        public async Task<IEnumerable<UserDto>> GetMembers(int surveyId){
            var userId = User.Claims.Where(x => x.Type == "Id").FirstOrDefault()?.Value;
            var users = await service.FindTeamMembersFromSurvey(surveyId, userId);
            return users.Select(user => mapper.Map<UserDto>(user));
        }

        /// <summary>
        /// Endpoint to get a list of all badges that can be awarded
        /// </summary>
        /// <returns>A list of badges that can be awarded in a survey</returns>
        [HttpGet("badges")]
        public async Task<IEnumerable<BadgeDto>> GetBadges(){
            var badges = await service.GetBadges();
            return badges.Select(badge => mapper.Map<BadgeDto>(badge));
        }

        /// <summary>
        /// Endpoint to get surveys that a student needs to complete
        /// </summary>
        /// <returns>A list of surveys that a student needs to complete</returns>
        [HttpGet("pending")]
        public async Task<IEnumerable<SurveyDto>> GetSurveysStudentNeedsToComplete(){
            var userId = User.Claims.Where(x => x.Type == "Id").FirstOrDefault()?.Value;
            var surveys = await service.GetSurveysStudentNeedsToComplete(userId);
            return surveys.Select(survey => mapper.Map<SurveyDto>(survey));
        }

        /// <summary>
        /// Endpoint allowing a student to answer a survey
        /// </summary>
        /// <param name="dto">The students survey answers</param>
        /// <param name="id">The ID of the survey that the student is answering</param>
        /// <returns></returns>
        [HttpPost("{id}/answer")]
        public async Task<ActionResult<SurveyDto>> AnswerSurvey(AnswerSurveyDto dto, int id)
        {
            var userId = User.Claims.Where(x => x.Type == "Id").FirstOrDefault()?.Value;
            var surveyAttempt = await service.AnswerSurvey(dto, id, userId);
            return CreatedAtAction(nameof(GetSurveys), new { id = surveyAttempt.Id }, mapper.Map<SurveyAttemptDto>(surveyAttempt));
        }
    }
}