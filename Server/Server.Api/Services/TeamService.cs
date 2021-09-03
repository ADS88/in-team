using System.Collections;
using System.Linq;
using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Server.Api.Dtos;

namespace Server.Api.Services
{
    public class TeamService: ITeamService
    {
        private readonly ITeamsRepository repository;
        private readonly UserManager<AppUser> userManager;

        public TeamService(ITeamsRepository repository, UserManager<AppUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<Team>> Get(){
            return await repository.GetAll();
        }
        public async Task<Team> GetById(int id){
            return await repository.Get(id);
        }

        public async Task<Boolean> DeleteTeam(int id){
            var team = await GetById(id);
            if(team is null){
                return false;
            }
            await repository.Delete(id);
            return true;
        }

        public async Task<Team> Create(string name, int courseId){
            Team team = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow,
                CourseId = courseId,
                Points = 0
            };

            await repository.Add(team);
            return team;
        }

        public async Task AddMember(int teamId, string memberId){
            await repository.AddMember(teamId, memberId);
        }

        public async Task AchieveStates(AchievedStateDto dto, int teamId, int iterationid){
            await repository.AddPoints(teamId, dto.Points);
            var achievedStates = dto.AchievedStates.Select(achievedState => new AchievedState()
                {
                    TeamId = teamId,
                    IterationId = iterationid,
                    AlphaId = achievedState.AlphaId,
                    StateId = achievedState.StateId,
                    AchievedDate = DateTimeOffset.UtcNow
                });
            await repository.AchieveStates(achievedStates);
        }

        public async Task<AchievedStateResponseDto> GetTeamsCurrentStates(int teamId){
            var achievedStates = await repository.GetTeamsAchievedStates(teamId);
            Dictionary<int, List<AchievedState>> alphaToAchievedStates = new();
            foreach(var s in achievedStates){
                if(!alphaToAchievedStates.ContainsKey(s.AlphaId)){
                    alphaToAchievedStates[s.AlphaId] = new List<AchievedState>();
                }
                alphaToAchievedStates[s.AlphaId].Add(s);
            }
            List<AchievedStateResponse> result = new();
            foreach(var item in alphaToAchievedStates){
                var mostRecentStateAchieved = item.Value.Where(a => a.AchievedDate == item.Value.Max(achievedState => achievedState.AchievedDate))
                                                .FirstOrDefault();
                
                result.Add(new(){
                    Id = mostRecentStateAchieved.Id,
                    AlphaName = mostRecentStateAchieved.Alpha.Name,
                    StateName = mostRecentStateAchieved.State.Name,
                    AchievedDate = mostRecentStateAchieved.AchievedDate
                });
            }
            return new AchievedStateResponseDto(){AchievedStates = result};
        }

        public async Task<TeamSurveyAnswerSummaryDto> GetTeamsSurveyAnswerSummaries(int teamId, int iterationId){
            var teamsSurveyAttemptsInIteration = await repository.GetTeamsSurveyAnswerSummaries(teamId, iterationId);
            var alphaToAnswer = teamsSurveyAttemptsInIteration
            .SelectMany(attempt => attempt.Answers)
            .GroupBy(answer => answer.Question.State.Alpha);
            var alphaSummaries = new List<TeamSurveyAlphaDto>();
            foreach(var alphaGroup in alphaToAnswer){
                var alpha = alphaGroup.Key;
                var answersRelatedToAlpha = alphaGroup.ToList();
                alphaSummaries.Add(new(){
                    AlphaId = alpha.Id,
                    AlphaName = alpha.Name,
                    States = GetStateSummaries(alpha, answersRelatedToAlpha)
                });
            }
            TeamSurveyAnswerSummaryDto dto = new(){
                Alphas = alphaSummaries
            };

            return dto;
        }

        private List<TeamSurveyStateDto> GetStateSummaries(Alpha alpha, ICollection<Answer> answers){
            var statesToAnswer = answers.GroupBy(answer => answer.Question.State);
            var stateSummaries = new List<TeamSurveyStateDto>();
            foreach(var stateGroup in statesToAnswer){
                var state = stateGroup.Key;
                var answersRelatedToState = stateGroup.ToList();
                var answerSummaries = GetAnswerSummaries(answersRelatedToState);
                var average = answerSummaries.Select(answer => answer.Average).DefaultIfEmpty(0).Average();
                stateSummaries.Add(new(){
                    StateId = state.Id,
                    StateName = state.Name,
                    Average = average,
                    AnswerSummaries = answerSummaries
                });
            }
            return stateSummaries;
        }

        private List<TeamSurveyQuestionDto> GetAnswerSummaries(ICollection<Answer> answers){
            var answerSummaries = new List<TeamSurveyQuestionDto>();
            var questionsToAnswer = answers.GroupBy(answer => answer.Question.Content);
            foreach(var questionGroup in questionsToAnswer){
                var questionContent = questionGroup.Key;
                var answersForQuestion = questionGroup.ToList();
                answerSummaries.Add(new(){
                    Content = questionContent,
                    Average = answersForQuestion.Select(answer => answer.LikertRating).DefaultIfEmpty(0).Average()
                });
            }
            return answerSummaries;
        }  
    }

    public record QuestionSummary {
        public string QuestionContent {get; init; }
        public List<int> Answers {get; init; }
        public int AlphaId {get; init;}
    }

    public record QuestionAverageSummary {
        public string QuestionContent {get; init; }
        public double AverageAnswer { get; init; }
    }

}
