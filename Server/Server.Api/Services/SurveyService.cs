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

        public async Task<Survey> Get(int id){
            return await surveysRepository.Get(id);
        }

        public async Task<IEnumerable<Survey>> GetAll(){
            return await surveysRepository.GetAll();
        }

        public async Task<IEnumerable<Badge>> GetBadges(){
            return await surveysRepository.GetBadges();
        }

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
