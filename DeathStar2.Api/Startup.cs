using DeathStar2.Data;
using DeathStar2.Data.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeathStar2.Services;
using DeathStar2.Services.Contracts;


namespace DeathStar2.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IKnownTargetsRepository>((IServiceProvider provider) =>
            {
                return new KnownTargetsRepository()
                {
                    PATH = "deathstardb.db"
                };
            });

            services.AddScoped<IHatchRepository>((IServiceProvider provider) =>
            {
                return new HatchRepository()
                {
                    PATH = "deathstardb.db"
                };
            });

            services.AddScoped<IKnownTargetsRepository>((IServiceProvider provider) =>
            {
                return new KnownTargetsRepository()
                {
                    PATH = "deathstardb.db"
                };
            });


            services.AddScoped<ISuperLaserRepository>((IServiceProvider provider) =>
            {
                return new SuperLaserRepository()
                {
                    PATH = "deathstardb.db"
                };
            });

            services.AddScoped<ISuperLaserService, SuperLaserService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() { Title = "DeathStar2", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    In = "header",
                    Description = "Identify yourself. What's your Bearer token?",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", Enumerable.Empty<string>() }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        var sharedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("i-find-your-lack-of-security-disturbing"));

                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ClockSkew = TimeSpan.FromMinutes(5),
                            RequireExpirationTime = true,
                            ValidateLifetime = true,
                            ValidateAudience = true,
                            ValidAudience = "",
                            ValidateIssuer = true,
                            ValidIssuer = "",
                            ValidateIssuerSigningKey = true,
                            RequireSignedTokens = true,
                            IssuerSigningKey = sharedKey
                        };
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Death Star 2");
            });
        }
    }
}
