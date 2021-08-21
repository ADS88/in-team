using System.Collections;
using System;
using Server.Api.Repositories;
using Server.Api.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server.Api.Services
{
    public class SurveyService: ISurveyService
    {

        private readonly ISurveysRepository surveysRepository;
        private readonly IAlphasRepository alphasRepository;
        private readonly ITeamsRepository teamsRepository;
        public SurveyService(ISurveysRepository surveysRepository, IAlphasRepository alphasRepository, ITeamsRepository teamsRepository)
        {
            this.surveysRepository = surveysRepository;
            this.alphasRepository = alphasRepository;
            this.teamsRepository = teamsRepository;
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

        public async Task<Survey> Get(int id){
            return await surveysRepository.Get(id);
        }

        public async Task<IEnumerable<Survey>> GetAll(){
            return await surveysRepository.GetAll();
        }

    }
}
