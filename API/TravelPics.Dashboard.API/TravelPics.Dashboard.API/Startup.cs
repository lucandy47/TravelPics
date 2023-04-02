using TravelPics.Domains.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TravelPics.Users;
using TravelPics.Users.Abstraction;
using TravelPics.Users.Repository;
using TravelPics.Users.Profiles;
using TravelPics.Security;
using TravelPics.Security.Models;
using Microsoft.AspNetCore.Authentication.Certificate;
using TravelPics.Documents.Configs;
using TravelPics.Documents;
using TravelPics.Documents.Repositories;
using TravelPics.Documents.Abstraction;

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
            services.AddAuthentication(
                CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate();

            services.AddCors();
            var connectionString = Configuration.GetConnectionString("TravelPicsDB");

            services.AddSingleton(Configuration.GetSection("Jwt").Get<AuthorizationConfiguration>());

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddScoped<IDocumentsRepository, DocumentsRepository>();
            services.AddScoped<IDocumentsService, DocumentsService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddAzureAppConfiguration();

            services.AddOptions<BlobContainerConfig>()
                .Configure(options =>
                {
                    options.StorageConnectionString = Configuration.GetValue<string>("ConnectionStrings:BlobStorage");
                }).ValidateDataAnnotations();

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

            var cors = Configuration.GetValue<string>("Dashboard:ApiCors");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(policy => policy.WithOrigins(
                cors.Split(",").Select(site=>site.Trim()).ToArray()
            ).AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
