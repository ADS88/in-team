using System;
using System.Collections.Generic;
using System.Linq;
using Server.Entities;
using Server.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDataContext context;
        public UsersRepository(IDataContext context){
            this.context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> Get(Guid guid)
        {
            return await context.Users.FindAsync(guid);
        }

        public async Task Add(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            var userToUpdate = await context.Users.FindAsync(user.Id);
            if(userToUpdate == null){
                throw new NullReferenceException();
            }

            //TODO: Update work here

        }

        public async Task Delete(Guid id)
        {
            var userToDelete = await context.Users.FindAsync(id);
            if(userToDelete == null){
                throw new NullReferenceException();
            }
            context.Users.Remove(userToDelete);;
            await context.SaveChangesAsync();
        }
    }
}
