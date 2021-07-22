using System;
using System.Collections.Generic;
using System.Linq;
using Server.Entities;

namespace Server.Repositories
{
    public class InMemoryUsersRepository : IUsersRepository
    {
        private readonly List<User> users = new()
        {
            new User { Id = Guid.NewGuid(), FirstName = "Steven", LastName = "Stevens", CreatedDate = DateTimeOffset.UtcNow },
            new User { Id = Guid.NewGuid(), FirstName = "Andrew", LastName = "Sturman", CreatedDate = DateTimeOffset.UtcNow },
            new User { Id = Guid.NewGuid(), FirstName = "Shavee", LastName = "Somaweera", CreatedDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<User> GetUsers()
        {
            return users;
        }

        public User GetUser(Guid guid)
        {
            return users.Where(item => item.Id == guid).SingleOrDefault();
        }

        public void CreateUser(User user)
        {
            users.Add(user);
        }

        public void UpdateUser(User user)
        {
            var index = users.FindIndex(existingItem => existingItem.Id == user.Id);
            users[index] = user;
        }

        public void DeleteUser(Guid id)
        {
            var index = users.FindIndex(existingUser => existingUser.Id == id);
            users.RemoveAt(index);
        }
    }
}
