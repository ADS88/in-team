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

        public async Task<Alpha> GetAlpha(int id)
        {
            return await context.Alphas.Include(alpha => alpha.States).FirstOrDefaultAsync(alpha => alpha.Id == id);
        }

        public async Task<State> GetState(int id)
        {
            return await context.States.Include(state => state.Questions).FirstOrDefaultAsync(state => state.Id == id);
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