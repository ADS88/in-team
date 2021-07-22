using System;
using System.Collections.Generic;
using Server.Entities;
using System.Threading.Tasks;

namespace Server.Repositories
{
    public interface IUsersRepository
    {
        Task<User> Get(Guid guid);
        Task<IEnumerable<User>> GetAll();
        Task Add(User item);
        Task Delete(Guid id);
        Task Update(User id);
    }
}