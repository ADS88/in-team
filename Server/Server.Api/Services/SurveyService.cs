using System;
using Server.Api.Repositories;
using Server.Api.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Server.Api.Dtos;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Server.Api.Services
{
    public class SurveyService: ISurveyService
    {

        private readonly ISurveysRepository surveysRepository;
        private readonly IAlphasRepository alphasRepository;
        private readonly ITeamsRepository teamsRepository;
        private readonly IUserRepository userRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly ILogger<SurveyService> _logger;
        public SurveyService(ISurveysRepository surveysRepository, IAlphasRepository alphasRepository, ITeamsRepository teamsRepository, IUserRepository userRepository, UserManager<AppUser> userManager, ILogger<SurveyService> logger)
        {
            this.surveysRepository = surveysRepository;
            this.alphasRepository = alphasRepository;
            this.teamsRepository = teamsRepository;
            this.userRepository = userRepository;
            this.userManager = userManager;
            this._logger = logger;
        }

        /// <summary>
        /// Creates a new survey
        /// </summary>
        /// <param name="name">The name of the survey</param>
        /// <param name="stateIds">The IDs of states assessed in this survey</param>
        /// <param name="TeamIds">The IDs of teams the survey will be distributed to</param>
        /// <param name="start">The date which the survey opens for answers</param>
        /// <param name="end">The date which the survey closes for answers</param>
        /// <param name="iterationId">The iteration that the survey relates to</param>
        /// <returns></returns>
        public async Task<Survey> Create(string name, ICollection<int> stateIds, ICollection<int> TeamIds, DateTimeOffset start, DateTimeOffset end, int iterationId){
            var questions = new List<Question>();
            foreach(var stateId in stateIds){
                var state = await alphasRepository.GetState(stateId);
                questions.AddRange(state.Questions);
            }
            var teams = new List<Team>();
            foreach(var teamId in TeamIds){
                var team = await teamsRepository.Get(teamId);
                teams.Add(team);
            }
            Survey survey = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow,
                OpeningDate = start,
                ClosingDate = end,
                Questions = questions,
                Teams = teams,
                IterationId = iterationId
            };

            await surveysRepository.Create(survey);
            return survey;
        }

        /// <summary>
        /// Logic for a student to answer a survey
        /// </summary>
        /// <param name="dto">a DTO containing the users answers and badges given</param>
        /// <param name="surveyId">The ID of the survey to be answered</param>
        /// <param name="userId">The ID of the user who is answering the survey</param>
        /// <returns></returns>
        public async Task<SurveyAttempt> AnswerSurvey(AnswerSurveyDto dto, int surveyId, string userId){

            var user = await userManager.FindByIdAsync(userId);

            if(user == null || await surveysRepository.GetAttempt(surveyId, userId) != null){
                return null;
            }

            SurveyAttempt surveyAttempt = new (){
                SurveyId = surveyId,
                AppUser = user,
                CompletedDate = DateTimeOffset.UtcNow,
            };

            await surveysRepository.CreateSurveyAttempt(surveyAttempt);


            var answers = dto.Answers.Select(answer => new Answer(){
                LikertRating = answer.LikertRating,
                QuestionId = answer.QuestionId,
                Attempt = surveyAttempt,
            });

            await surveysRepository.AddAnswers(answers);

            var badgeGifts = dto.BadgeGifts
                .Select(async badgeGift => new BadgeGift(){
                BadgeId = badgeGift.BadgeId,
                SurveyAttempt = surveyAttempt,
                User = await userManager.FindByIdAsync(badgeGift.UserId)
                }
                ).Select(t => t.Result);

       
            await surveysRepository.AddBadgeGifts(badgeGifts);

            var team = await FindTeamFromSurveyAndUser(surveyId, userId);
            await teamsRepository.AddPoints(team.Id, 10);

            return surveyAttempt;
        }

        /// <summary>
        /// Gets a specific survey by ID
        /// </summary>
        /// <param name="id">The ID of the survey</param>
        /// <returns>A survey, or null</returns>
        public async Task<Survey> Get(int id){
            return await surveysRepository.Get(id);
        }

        /// <summary>
        /// Gets all surveys in the application
        /// </summary>
        /// <returns>An enumerable of surveys</returns>
        public async Task<IEnumerable<Survey>> GetAll(){
            return await surveysRepository.GetAll();
        }

        /// <summary>
        /// Gets a list of all badges in the application
        /// </summary>
        /// <returns>An enumerable of badges</returns>
        public async Task<IEnumerable<Badge>> GetBadges(){
            return await surveysRepository.GetBadges();
        }

        /// <summary>
        /// Find team members from a specific survey. Used to figure out who users
        /// Can gift badges to when completing surveys
        /// </summary>
        /// <param name="surveyId">The ID of the survey</param>
        /// <param name="userId">The ID of the user completing the survey</param>
        /// <returns>An enumerable of users</returns>
        public async Task<IEnumerable<AppUser>> FindTeamMembersFromSurvey(int surveyId, string userId){
            var survey = await surveysRepository.GetSurveyWithTeams(surveyId);
            var members = new List<AppUser>();
            foreach(var team in survey.Teams){
                foreach(var member in team.Members){
                    if(member.Id == userId){
                        members.AddRange(team.Members);
                    }
                }
            }
            return members.Where(m => m.Id != userId);
            
        }

        /// <summary>
        /// Finds the team related to a user and survey
        /// </summary>
        /// <param name="surveyId">The ID of the survey</param>
        /// <param name="userId">The ID of the user</param>
        /// <returns>The team that the survey relates to, or null</returns>
        private async Task<Team> FindTeamFromSurveyAndUser(int surveyId, string userId){
            var survey = await surveysRepository.GetSurveyWithTeams(surveyId);
             foreach(var team in survey.Teams){
                foreach(var member in team.Members){
                    if(member.Id == userId){
                        return team;
                    }
                }
             }
            return null;
        }

        /// <summary>
        /// Gets a list of surveys that a student needs to complete
        /// </summary>
        /// <param name="userId">The ID of the student</param>
        /// <returns>An enumerable of surveys</returns>
        public async Task<IEnumerable<Survey>> GetSurveysStudentNeedsToComplete(string userId){
            var user = await userRepository.GetUserWithTeams(userId);
            var surveysAssignedToStudent = await surveysRepository.GetSurveysAssignedToStudent(user);
            surveysAssignedToStudent = surveysAssignedToStudent
                                        .Where(s => DateTimeOffset.Compare(s.ClosingDate, DateTimeOffset.UtcNow) > 0)
                                        .Where(s => DateTimeOffset.Compare(DateTimeOffset.UtcNow, s.OpeningDate) > 0)
                                        .ToList();
            var surveyAttemptsFromStudent = await surveysRepository.GetAttemptsFromUser(userId);
            var attemptedSurveyIds = new HashSet<int>(surveyAttemptsFromStudent.Select(s => s.SurveyId));
            return surveysAssignedToStudent.Where(s => !attemptedSurveyIds.Contains(s.Id));
        }
    }
}
