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
    /// <summary>
    /// Business logic for team related operations
    /// </summary>
    public class TeamService : ITeamService
    {
        private readonly ITeamsRepository repository;
        private readonly UserManager<AppUser> userManager;

        public TeamService(ITeamsRepository repository, UserManager<AppUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        /// <summary>
        /// Gets all teams within the application
        /// </summary>
        /// <returns>An enumerable of teams</returns>
        public async Task<IEnumerable<Team>> Get()
        {
            return await repository.GetAll();
        }

        /// <summary>
        /// Gets a specific team by ID
        /// </summary>
        /// <param name="id">The ID of the team to get</param>
        /// <returns>The team if it exists, otherwise null</returns>
        public async Task<Team> GetById(int id)
        {
            return await repository.Get(id);
        }

        /// <summary>
        /// Deletes a team from the application
        /// </summary>
        /// <param name="id">The ID of the team to delete</param>
        /// <returns>True if the team was successfully deleted, otherwise false</returns>
        public async Task<Boolean> DeleteTeam(int id)
        {
            var team = await GetById(id);
            if (team is null)
            {
                return false;
            }
            await repository.Delete(id);
            return true;
        }

        /// <summary>
        /// Creates a new team
        /// </summary>
        /// <param name="name">The name of the team</param>
        /// <param name="courseId">The ID of the course the team belongs to</param>
        /// <returns>The created team</returns>
        public async Task<Team> Create(string name, int courseId)
        {
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

        /// <summary>
        /// Adds a new member to a team
        /// </summary>
        /// <param name="teamId">The ID of the team to add a member to</param>
        /// <param name="memberId">The ID of the user to add to the team</param>
        /// <returns></returns>
        public async Task AddMember(int teamId, string memberId)
        {
            await repository.AddMember(teamId, memberId);
        }

        /// <summary>
        /// Used by the teaching team when grading iterations.
        /// Marks what states a team has achieved for each alpha for an interation
        /// </summary>
        /// <param name="dto">DTO containing states the teaching team has assigned</param>
        /// <param name="teamId">The ID of the team being graded</param>
        /// <param name="iterationid">The ID of the iteration being assessed</param>
        /// <returns></returns>
        public async Task AchieveStates(AchievedStateDto dto, int teamId, int iterationid)
        {
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

        /// <summary>
        /// Gets the most recently achieved state for each alpha by the team
        /// </summary>
        /// <param name="teamId">The ID of the team</param>
        /// <returns>What states the team is currently in for each alpha they have been assessed on</returns>
        public async Task<AchievedStateResponseDto> GetTeamsCurrentStates(int teamId)
        {
            var achievedStates = await repository.GetTeamsAchievedStates(teamId);
            Dictionary<int, List<AchievedState>> alphaToAchievedStates = new();
            foreach (var s in achievedStates)
            {
                if (!alphaToAchievedStates.ContainsKey(s.AlphaId))
                {
                    alphaToAchievedStates[s.AlphaId] = new List<AchievedState>();
                }
                alphaToAchievedStates[s.AlphaId].Add(s);
            }
            List<AchievedStateResponse> result = new();
            foreach (var item in alphaToAchievedStates)
            {
                var mostRecentStateAchieved = item.Value.Where(a => a.AchievedDate == item.Value.Max(achievedState => achievedState.AchievedDate))
                                                .FirstOrDefault();

                result.Add(new()
                {
                    Id = mostRecentStateAchieved.Id,
                    AlphaName = mostRecentStateAchieved.Alpha.Name,
                    StateName = mostRecentStateAchieved.State.Name,
                    AchievedDate = mostRecentStateAchieved.AchievedDate
                });
            }
            return new AchievedStateResponseDto() { AchievedStates = result };
        }

        /// <summary>
        /// Gets an average of each answer, and state for state they have been surveyed with
        /// </summary>
        /// <param name="teamId">The ID of the team</param>
        /// <param name="iterationId">The iteration we are getting averages for</param>
        /// <returns>A summary of the averages of the teams surveys for the given iteration</returns>
        public async Task<TeamSurveyAnswerSummaryDto> GetTeamsSurveyAnswerSummaries(int teamId, int iterationId)
        {
            var teamsSurveyAttemptsInIteration = await repository.GetTeamsSurveyAnswerSummaries(teamId, iterationId);
            var alphaToAnswer = teamsSurveyAttemptsInIteration
            .SelectMany(attempt => attempt.Answers)
            .GroupBy(answer => answer.Question.State.Alpha);
            var alphaSummaries = new List<TeamSurveyAlphaDto>();
            foreach (var alphaGroup in alphaToAnswer)
            {
                var alpha = alphaGroup.Key;
                var answersRelatedToAlpha = alphaGroup.ToList();
                alphaSummaries.Add(new()
                {
                    AlphaId = alpha.Id,
                    AlphaName = alpha.Name,
                    States = GetStateSummaries(alpha, answersRelatedToAlpha)
                });
            }
            TeamSurveyAnswerSummaryDto dto = new()
            {
                Alphas = alphaSummaries
            };

            return dto;
        }

        /// <summary>
        /// Gets a summary of a each state of an alpha
        /// </summary>
        /// <param name="alpha">The alpha we are getting a summary of</param>
        /// <param name="answers">The answers provided by students for that alpha</param>
        /// <returns>Average for each state within that alpha</returns>
        private List<TeamSurveyStateDto> GetStateSummaries(Alpha alpha, ICollection<Answer> answers)
        {
            var statesToAnswer = answers.GroupBy(answer => answer.Question.State);
            var stateSummaries = new List<TeamSurveyStateDto>();
            foreach (var stateGroup in statesToAnswer)
            {
                var state = stateGroup.Key;
                var answersRelatedToState = stateGroup.ToList();
                var answerSummaries = GetAnswerSummaries(answersRelatedToState);
                var average = answerSummaries.Select(answer => answer.Average).DefaultIfEmpty(0).Average();
                stateSummaries.Add(new()
                {
                    StateId = state.Id,
                    StateName = state.Name,
                    Average = average,
                    AnswerSummaries = answerSummaries
                });
            }
            return stateSummaries;
        }

        /// <summary>
        /// Gets the average answer for each question of the survey
        /// </summary>
        /// <param name="answers">The teams answers</param>
        /// <returns>A summary of a state and questions related to an alpha</returns>
        private List<TeamSurveyQuestionDto> GetAnswerSummaries(ICollection<Answer> answers)
        {
            var answerSummaries = new List<TeamSurveyQuestionDto>();
            var questionsToAnswer = answers.GroupBy(answer => answer.Question.Content);
            foreach (var questionGroup in questionsToAnswer)
            {
                var questionContent = questionGroup.Key;
                var answersForQuestion = questionGroup.ToList();
                answerSummaries.Add(new()
                {
                    Content = questionContent,
                    Average = answersForQuestion.Select(answer => answer.LikertRating).DefaultIfEmpty(0).Average()
                });
            }
            return answerSummaries;
        }
    }

    /// <summary>
    /// A questions content, average answer, and related alpha
    /// </summary>
    public record QuestionSummary
    {
        public string QuestionContent { get; init; }
        public List<int> Answers { get; init; }
        public int AlphaId { get; init; }
    }

    /// <summary>
    /// The teams average answer for a specific question asked in surveys
    /// </summary>
    public record QuestionAverageSummary
    {
        public string QuestionContent { get; init; }
        public double AverageAnswer { get; init; }
    }

}
