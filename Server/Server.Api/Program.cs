using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Server.Api.Data;
using Microsoft.Extensions.Logging;


namespace Server.Api
{
    public class Program
    {
        /// <summary>
        /// Main entry point to the program
        /// </summary>
        /// <param name="args">Arguments passed in through the command line</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
            .Build()
            .MigrateDatabase<DataContext>()
            .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                logging.ClearProviders();
                logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
