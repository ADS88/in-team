using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Server.Api
{
    /// <summary>
    /// Used to automatically migrate the hosted database on Heroku when the entity classes change
    /// </summary>
    public static class IWebHostExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost webHost) where T : DbContext{
            using(var scope = webHost.Services.CreateScope()){
                var services = scope.ServiceProvider;
                var db = services.GetRequiredService<T>();
                db.Database.Migrate();
            }
            return webHost;
        }
    }
}