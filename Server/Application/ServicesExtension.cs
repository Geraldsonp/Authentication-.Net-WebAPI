using System.Text;
using Application.Configuration;
using Application.DAO;
using Application.Domain;
using Application.Handlers;
using Application.Interfaces;
using Application.Services;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public static class ServicesExtension 
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
       
        TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);

        services.AddDbContext<ApiDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        services.AddIdentityCore<User>().AddEntityFrameworkStores<ApiDbContext>();

        services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {

                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });


        services.AddScoped<IUserCreationHandler, UserCreationHandler>();
        services.AddScoped<IUserEditHandler, UserEditHandler>();
        services.AddScoped<IUserProfileHandler, UserProfileHandler>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}