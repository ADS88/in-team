using Microsoft.EntityFrameworkCore;
using Server.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Server.Data
{
    public class DataContext: IdentityDbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
             
        }
 
        public DbSet<Course> Courses { get; init; }
    }
}