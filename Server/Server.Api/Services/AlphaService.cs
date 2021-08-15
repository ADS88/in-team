using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Services
{
    public class AlphaService: IAlphaService
    {

        private readonly IAlphasRepository repository;

        public AlphaService(IAlphasRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Alpha> CreateAlpha(string name){
            Alpha alpha = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repository.AddAlpha(alpha);
            return alpha;
        }

        public async Task<State> AddState(string name, int alphaId){
            State state = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow,
                AlphaId = alphaId
            };
            await repository.AddState(state);
            return state;
        }

        public async Task<Question> AddQuestion(string content, int stateId){
            Question question = new()
            {
                Content = content,
                CreatedDate = DateTimeOffset.UtcNow,
                StateId = stateId
            };
            await repository.AddQuestion(question);
            return question;
        }

        public async Task<IEnumerable<Alpha>> GetAlphas(){
            var alphas = await repository.GetAlphas();
            if(alphas is null) throw new NullReferenceException();
            return alphas;
        }

        public async Task<Alpha> GetAlpha(int id){
            var alpha = await repository.GetAlpha(id);
            if(alpha is null) throw new NullReferenceException();
            return alpha;
        }

        public async Task<IEnumerable<Question>> GetQuestions(int stateId){
            var questions = await repository.GetQuestions(stateId);
            if(questions is null) throw new NullReferenceException();
            return questions;
        }
    }
}