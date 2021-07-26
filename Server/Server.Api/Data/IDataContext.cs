using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Api.Entities;
 
namespace Server.Api.Data
{
    public interface IDataContext
    {
        DbSet<Course> Courses { get; init; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}