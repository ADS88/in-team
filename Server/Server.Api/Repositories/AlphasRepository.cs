
using System.Collections.Generic;
using Server.Api.Entities;
using Server.Api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Repositories
{
    /// <summary>
    /// Repository class for alpha database operations
    /// </summary>
    public class AlphasRepository: IAlphasRepository
    {
        private readonly IDataContext context;
        public AlphasRepository(IDataContext context){
            this.context = context;
        }

        /// <summary>
        /// Gets a list of all alphas in the database
        /// </summary>
        /// <returns>a list of all alphas</returns>
        public async Task<IEnumerable<Alpha>> GetAlphas()
        {
            return await context.Alphas.ToListAsync();
        }

        /// <summary>
        /// Gets a single alpha from the database
        /// </summary>
        /// <param name="id">the ID of the alpha to get</param>
        /// <returns>The first alpha with that ID, or null</returns>
        public async Task<Alpha> GetAlpha(int id)
        {
            return await context.Alphas.Include(alpha => alpha.States).FirstOrDefaultAsync(alpha => alpha.Id == id);
        }

        /// <summary>
        /// Gets a single state from the database
        /// </summary>
        /// <param name="id">the ID of the state to get</param>
        /// <returns>The first state with that ID, or null</returns>
        public async Task<State> GetState(int id)
        {
            return await context.States.Include(state => state.Questions).FirstOrDefaultAsync(state => state.Id == id);
        }

        /// <summary>
        /// Adds an alpha to the database
        /// </summary>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public async Task AddAlpha(Alpha alpha)
        {
            context.Alphas.Add(alpha);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds a question to the database
        /// </summary>
        /// <param name="question">The question to add</param>
        /// <returns></returns>
        public async Task AddQuestion(Question question)
        {
            context.Questions.Add(question);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds a state to the database
        /// </summary>
        /// <param name="state">The state to add</param>
        /// <returns></returns>
        public async Task AddState(State state)
        {
            context.States.Add(state);
            await context.SaveChangesAsync();
        }
    }
}