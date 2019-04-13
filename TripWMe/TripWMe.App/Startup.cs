using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TripWMe.Data;
using TripWMe.Data.Repositories;
using TripWMe.Data.RepositoryInterfaces;

namespace TripWMe.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddDbContext<TripWMeContext>(cfg =>
            {
                if (Environment.IsDevelopment())
                {
                    cfg.UseSqlServer(Configuration.GetConnectionString("ProductRoadMapConnectionString"));
                }
                else
                {
                    cfg.UseSqlServer(Configuration.GetConnectionString("ProductRoadMapConnectionStringProd"));
                }
            });
            services.AddTransient<TripWMeSeeder>();
            services.AddScoped<ITripRepository, TripRepository>();
            services.AddAutoMapper();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", Action = "Index" });
            });

            // Seed the database
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var recreateDbOption = Configuration.GetSection("DevelopmentEnvironmentSettings").GetSection("RecreateDatabaseEachTime").Value;
                var seeder = scope.ServiceProvider.GetService<TripWMeSeeder>();
                seeder.Seed(recreateDbOption).Wait();
            }
        }
    }
}
