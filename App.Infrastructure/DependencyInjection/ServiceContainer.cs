using App.Application.Interfaces;
using App.Infrastructure.Data;
using App.Infrastructure.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace App.Infrastructure.DependencyInjection {
    public static class ServiceContainer {
        public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration) {

            //adding 
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")
            , b => b.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName)),// Tells the Entity Core Framework should look for or place migration files. Seems like it  places migrations in same project even if b =>b.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName)is notused
            ServiceLifetime.Scoped);

            //Source https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-9.0
            services.AddAuthentication(options => {
                //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))

                };
            });
            services.AddScoped<IUser, UserRepo>();
            return services;
        }
    }
}
