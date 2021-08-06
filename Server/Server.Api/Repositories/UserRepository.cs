using System;
using System.Collections.Generic;
using System.Linq;
using Server.Api.Entities;
using Server.Api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataContext context;
        public UserRepository(IDataContext context){
            this.context = context;
        }

        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await context.AppUsers.ToListAsync();
        }

    }
}
