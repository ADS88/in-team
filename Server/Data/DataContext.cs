using Microsoft.EntityFrameworkCore;
using Server.Entities;
 
namespace Server.Data
{
    public class DataContext: DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
             
        }
 
        public DbSet<User> Users { get; init; }
    }
}