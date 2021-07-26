using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

namespace Server.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            var connectionString = Configuration.GetSection(nameof(PostgresSettings)).Get<PostgresSettings>().ConnectionString;
            services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>());

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt => {
                var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);
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
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<ICoursesRepository, CoursesRepository>();
            services.AddControllers();
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


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Server v1"));
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            CreateRoles(roleManager);
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
