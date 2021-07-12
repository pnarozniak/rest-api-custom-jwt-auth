using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using rest_api_custom_jwt_auth.Data;
using rest_api_custom_jwt_auth.Models.Configurations;

namespace rest_api_custom_jwt_auth
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
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MsSqlDb"));
            });

            services.Configure<JwtConfiguration>(
                Configuration.GetSection(nameof(JwtConfiguration)));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtConfiguration = Configuration.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = jwtConfiguration.ValidAudience,
                        ValidateAudience = true,
                        ValidIssuer = jwtConfiguration.ValidIssuer,
                        ValidateIssuer = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey)),
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true,
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = jwtConfiguration.OnAuthenticationFailedHandler,
                        OnTokenValidated = jwtConfiguration.OnTokenValidatedHandler
                    };
                });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "rest_api_custom_jwt_auth", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "rest_api_custom_jwt_auth v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
