using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using System;
using System.Linq;
using System.Text;
using TripWMe.App.Auth;
using TripWMe.Data;
using TripWMe.Data.Repositories;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Domain.User;
using TripWMe.Models.Authentication;

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

            services.AddSingleton<IJwtFactory, JwtFactory>();

            // jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));
            
            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });


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
                    jsonInputFormatter.SupportedMediaTypes.Add("application/vnd.tripwme.createnewusertoken+json");
                    jsonInputFormatter.SupportedMediaTypes.Add("application/vnd.tripwme.createnewuser+json");
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
            services.AddScoped<IGeoEntitiesRepository, GeoEntitiesRepository>();
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
