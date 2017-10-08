using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParkingApp.Data.Domain;
using ParkingApp.Data.Domain.Abstract;
using ParkingApp.Data.Domain.Identity;
using ParkingApp.Services;

namespace ParkingApp
{
    public class Startup
    {
        IConfigurationRoot _config;
        private IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .AddEnvironmentVariables();
            _config = builder.Build();
            _env = env;
        }

       
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            services.AddDbContext<ParkingDbContext>();
            services.AddTransient<IRepository, Repository<ParkingDbContext>>();
            services.AddTransient<ParkingIdentityInitializer>();
            services.AddTransient<ParkingDbInitializer>();


            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ParkingDbContext>()
                .AddDefaultTokenProviders();
            services.AddMemoryCache();

            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("SuperUsers", p => p.RequireClaim("SuperUser", "True"));
                cfg.AddPolicy("Users", p => p.RequireClaim("User", "True"));
            });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              ILoggerFactory loggerFactory,
                              ParkingIdentityInitializer identitySeeder,
                              ParkingDbInitializer seeder)
        {
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc(routes => routes.MapRoute(
                name: "default",
                template: "{controller}/{action=Index}/{id?}"));

            //Sample data pre-seed
            seeder.Seed().Wait();
            identitySeeder.Seed().Wait();
        }
    }
}
