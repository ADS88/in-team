using Microsoft.EntityFrameworkCore;
using Server.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Server.Api.Data
{
    public class DataContext: IdentityDbContext<AppUser>, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
             
        }
 
        public DbSet<Course> Courses { get; init; }
        public DbSet<Team> Teams { get; init; }
        public DbSet<AppUser> AppUsers { get; init; }
        public DbSet<Iteration> Iterations { get; init; }
        public DbSet<Alpha> Alphas { get; init; }
        public DbSet<State> States { get; init; }
        public DbSet<Question> Questions { get; init; }
        public DbSet<Survey> Surveys { get; init; }
    }
}