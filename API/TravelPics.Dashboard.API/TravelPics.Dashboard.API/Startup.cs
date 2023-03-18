using TravelPics.Domains.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TravelPics.Users;
using TravelPics.Users.Abstraction;
using TravelPics.Users.Repository;
using TravelPics.Users.Profiles;
using TravelPics.Security;
using TravelPics.Security.Models;

namespace TravelPics.Dashboard.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddCors();
            var connectionString = Configuration.GetConnectionString("TravelPicsDB");

            services.AddSingleton(Configuration.GetSection("Jwt").Get<AuthorizationConfiguration>());

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddAzureAppConfiguration();

            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(typeof(UserProfile));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(
                "http://localhost:4200"
            ));

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseAzureAppConfiguration();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
