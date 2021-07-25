using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Entities;
 
namespace Server.Data
{
    public interface IDataContext
    {
        DbSet<Course> Courses { get; init; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}