using System.Text;
using GSM.Shared.EnvironmentVariable;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GSM.Shared.Setup.Jwt.Extensions;

public static class JwtConfigures
{
    public static void ConfigureJwtSecurity(this IServiceCollection services)
    {
        services.AddAuthentication(i =>
        {
            i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            i.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(i =>
        {
            i.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = EnvironmentUtils.Get<string>("JWT_SETTINGS_ISSUER"),
                ValidAudiences = EnvironmentUtils.Get<string>("JWT_SETTINGS_AUDIENCE")?.Split(","),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentUtils.Get<string>("JWT_SETTINGS_KEY") ?? string.Empty)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });
    }
}