using System;
using System.Collections.Generic;
using Server.Entities;
using System.Threading.Tasks;

namespace Server.Repositories
{
    public interface IUsersRepository
    {
        User GetUser(Guid guid);
        IEnumerable<User> GetUsers();
        void CreateUser(User item);
        void UpdateUser(User item);
        void DeleteUser(Guid id);
    }
}