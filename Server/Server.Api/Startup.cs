using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Server.Api.Repositories;
using Server.Api.Data;
using Microsoft.EntityFrameworkCore;
using Server.Api.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Threading.Tasks;
using Server.Api.Enums;
using Server.Api.Services;
using Server.Api.Entities;
using System;
using Npgsql;


namespace Server.Api
{
    public class Startup
    {

        bool IsDevelopment;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            IsDevelopment = env.IsDevelopment();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();

            services.AddAutoMapper(typeof(Startup));

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));


            string connectionString;
            if(IsDevelopment){
                connectionString = Configuration.GetSection(nameof(PostgresSettings)).Get<PostgresSettings>().ConnectionString;
            } else {
                connectionString = GetProductionDatabaseUrl();
            }            

            services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>());
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt => {
                var key = IsDevelopment ?
                 Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"])
                 : Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"));
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false
                };
            });

            //Check this context if there is error
            services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<DataContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            });
            AddServices(services);
            AddRepositories(services);
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseExceptionHandler("/error");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Server v1"));
                app.UseHttpsRedirection();
            }


            CreateRoles(roleManager);
        }

        private void AddServices(IServiceCollection services){
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAlphaService, AlphaService>();
            services.AddScoped<ISurveyService, SurveyService>();
        }

        private void AddRepositories(IServiceCollection services){
            services.AddScoped<ICoursesRepository, CoursesRepository>();
            services.AddScoped<ITeamsRepository, TeamsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAlphasRepository, AlphasRepository>();
            services.AddScoped<ISurveysRepository, SurveysRepository>();
        }

        private string GetProductionDatabaseUrl(){
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };

            return builder.ToString() +  ";SSL Mode=Require;Trust Server Certificate=true";
        }

        private void CreateRoles(RoleManager<IdentityRole> roleManager){
            string[] roleNames = {Roles.STUDENT, Roles.LECTURER};
            foreach (var roleName in roleNames){
                Task<bool> roleExists = roleManager.RoleExistsAsync(roleName);
                roleExists.Wait();
                if (!roleExists.Result){
                    var roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                    roleResult.Wait();
                }
            }
        }
    }
}
