using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Server.Api.Services
{
    /// <summary>
    /// Business logic for Alphas
    /// </summary>
    public class AlphaService : IAlphaService
    {

        private readonly IAlphasRepository repository;

        public AlphaService(IAlphasRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Calls the repository to create a new Alpha
        /// </summary>
        /// <param name="name">The name of the new alpha</param>
        /// <returns>The created Alpha</returns>
        public async Task<Alpha> CreateAlpha(string name)
        {
            Alpha alpha = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repository.AddAlpha(alpha);
            return alpha;
        }

        /// <summary>
        /// Calls the repository to add a state to an alpha
        /// </summary>
        /// <param name="name">The name of the state to add</param>
        /// <param name="alphaId">The ID of the alpha to add the state to</param>
        /// <returns>The created state</returns>
        public async Task<State> AddState(string name, int alphaId)
        {
            State state = new()
            {
                Name = name,
                CreatedDate = DateTimeOffset.UtcNow,
                AlphaId = alphaId
            };
            await repository.AddState(state);
            return state;
        }

        /// <summary>
        /// Adds a question to the a state
        /// </summary>
        /// <param name="content">The content of the question</param>
        /// <param name="stateId">The ID of the state to add the question to</param>
        /// <returns>The Added question</returns>
        public async Task<Question> AddQuestion(string content, int stateId)
        {
            Question question = new()
            {
                Content = content,
                CreatedDate = DateTimeOffset.UtcNow,
                StateId = stateId
            };
            await repository.AddQuestion(question);
            return question;
        }

        /// <summary>
        /// Gets all alphas in the application
        /// </summary>
        /// <returns>An enumerable of alphas</returns>
        public async Task<IEnumerable<Alpha>> GetAlphas()
        {
            var alphas = await repository.GetAlphas();
            if (alphas is null) throw new NullReferenceException();
            return alphas;
        }

        /// <summary>
        /// Gets a specific alpha in the application
        /// </summary>
        /// <param name="id">The ID of the alpha to get</param>
        /// <returns>An alpha with the given ID if it exists</returns>
        public async Task<Alpha> GetAlpha(int id)
        {
            var alpha = await repository.GetAlpha(id);
            if (alpha is null) throw new NullReferenceException();
            return alpha;
        }

        /// <summary>
        /// Gets a specific state in the application
        /// </summary>
        /// <param name="id">The ID of the state to get</param>
        /// <returns>The state if it exists</returns>
        public async Task<State> GetState(int id)
        {
            var state = await repository.GetState(id);
            if (state is null) throw new NullReferenceException();
            return state;
        }
    }
}