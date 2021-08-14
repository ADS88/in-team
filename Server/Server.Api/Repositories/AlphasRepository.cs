using System;
using System.Collections.Generic;
using Server.Api.Entities;
using Server.Api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Server.Api.Repositories
{
    public class AlphasRepository: IAlphasRepository
    {
        private readonly IDataContext context;
        public AlphasRepository(IDataContext context){
            this.context = context;
        }

        public async Task<IEnumerable<Alpha>> GetAlphas()
        {
            return await context.Alphas.ToListAsync();
        }

        public async Task<IEnumerable<State>> GetStates(int alphaId)
        {
            return await context.States.Where(state => state.AlphaId == alphaId).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestions(int stateId)
        {
            return await context.Questions.Where(question => question.StateId == stateId).ToListAsync();
        }

        public async Task AddAlpha(Alpha alpha)
        {
            context.Alphas.Add(alpha);
            await context.SaveChangesAsync();
        }

        public async Task AddQuestion(Question question)
        {
            context.Questions.Add(question);
            await context.SaveChangesAsync();
        }

        public async Task AddState(State state)
        {
            context.States.Add(state);
            await context.SaveChangesAsync();
        }
    }
}