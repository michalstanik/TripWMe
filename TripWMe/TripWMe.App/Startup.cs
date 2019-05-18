using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using TripWMe.Data;
using TripWMe.Data.Repositories;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Domain.User;

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
            services.AddIdentity<TUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<TripWMeContext>();

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };
                }
                );

            services.AddMvc(setupAction => 
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

                var jsonOutputFormatter = setupAction.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();

                if (jsonOutputFormatter != null)
                {
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.tripwme.trip+json");
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.tripwme.tripwithtripmanager+json");
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.tripwme.tripwithstops+json");
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.tripwme.tripwithstopsandusers+json");
                }

                var jsonInputFormatter = setupAction.InputFormatters.OfType<JsonInputFormatter>().FirstOrDefault();

                if (jsonInputFormatter != null)
                {
                    jsonInputFormatter.SupportedMediaTypes.Add("application/vnd.tripwme.tripforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes.Add("application/vnd.marvin.tripwithmanagerforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes.Add("application/vnd.tripwme.tripwithstopsforcreation+json");
                }
            })
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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(cfg =>
            {
                cfg.MapRoute(
                    "API",
                    "api/{controller=*}/{action=*}/{id?}");


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
