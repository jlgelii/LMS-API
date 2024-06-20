using CleanArchitecture.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace CleanArchitecture.API.Configurations.Services
{
    public static class JwtServiceExtensions
    {
        public static void AddJwt(this IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    //ValidIssuer = "smesk.in",
                    //ValidAudience = "readers",
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = TokenHelper.GetSecurityKey(),
                };
            });
        }
    }
}
