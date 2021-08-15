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
        public SurveyService(ISurveysRepository surveysRepository, IAlphasRepository alphasRepository)
        {
            this.surveysRepository = surveysRepository;
            this.alphasRepository = alphasRepository;
        }

        public async Task<Survey> Create(string name, ICollection<int> stateIds, DateTimeOffset start, DateTimeOffset end){
            var questions = new List<Question>();
            foreach(var stateId in stateIds){
                var state = await alphasRepository.GetState(stateId);
                questions.AddRange(state.Questions);
            }
            Survey survey = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow,
                StartDate = start,
                EndDate = end,
                Questions = questions
            };

            await surveysRepository.Create(survey);
            return survey;
        }

        public async Task<Survey> Get(int id){
            return await surveysRepository.Get(id);
        }

    }
}
