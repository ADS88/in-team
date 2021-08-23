using System;
using Server.Api.Repositories;
using Server.Api.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Server.Api.Dtos;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Server.Api.Services
{
    public class SurveyService: ISurveyService
    {

        private readonly ISurveysRepository surveysRepository;
        private readonly IAlphasRepository alphasRepository;
        private readonly ITeamsRepository teamsRepository;
        private readonly IUserRepository userRepository;
        private readonly UserManager<AppUser> userManager;
        public SurveyService(ISurveysRepository surveysRepository, IAlphasRepository alphasRepository, ITeamsRepository teamsRepository, IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            this.surveysRepository = surveysRepository;
            this.alphasRepository = alphasRepository;
            this.teamsRepository = teamsRepository;
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public async Task<Survey> Create(string name, ICollection<int> stateIds, ICollection<int> TeamIds, DateTimeOffset start, DateTimeOffset end){
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
                Teams = teams
            };

            await surveysRepository.Create(survey);
            return survey;
        }

        public async Task<SurveyAttempt> AnswerSurvey(AnswerSurveyDto dto, int surveyId, string userId){

            var user = await userManager.FindByIdAsync(userId);

            if( await surveysRepository.GetAttempt(surveyId, userId) != null){
                Console.WriteLine("attempted already!!!");
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

            return surveyAttempt;
        }

        public async Task<Survey> Get(int id){
            return await surveysRepository.Get(id);
        }

        public async Task<IEnumerable<Survey>> GetAll(){
            return await surveysRepository.GetAll();
        }
        public async Task<IEnumerable<Survey>> GetSurveysStudentNeedsToComplete(string userId){
            var user = await userRepository.GetUserWithTeams(userId);
            var surveysAssignedToStudent = await surveysRepository.GetSurveysAssignedToStudent(user);
            var surveyAttemptsFromStudent = await surveysRepository.GetAttemptsFromUser(userId);
            var attemptedSurveyIds = new HashSet<int>(surveyAttemptsFromStudent.Select(s => s.Id));
            return surveysAssignedToStudent.Where(s => !attemptedSurveyIds.Contains(s.Id));
        }
    }
}
