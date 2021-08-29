using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Api.Entities;
 
namespace Server.Api.Data
{
    public interface IDataContext
    {
        DbSet<Course> Courses { get; init; }
        DbSet<Team> Teams { get; init; }
        DbSet<Iteration> Iterations { get; init; }
        DbSet<AppUser> AppUsers { get; init; }
        DbSet<Alpha> Alphas { get; init; }
        DbSet<State> States { get; init; }
        DbSet<Question> Questions { get; init; }
        DbSet<Survey> Surveys { get; init; }
        DbSet<Answer> Answers { get; init; }
        DbSet<SurveyAttempt> SurveyAttempts { get; init; }
        DbSet<Badge> Badges { get; init; }
        DbSet<BadgeGift> BadgeGifts { get; init; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}