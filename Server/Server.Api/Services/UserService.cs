using Server.Api.Entities;
using Server.Api.Repositories;
using Server.Api.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server.Api.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<AppUser>> GetAll(){
            return await repository.GetAll();
        }
    }
}