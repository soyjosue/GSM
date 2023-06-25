using System.Reflection;
using System.Text;
using GSM.Shared.EnvironmentVariable;
using GSM.Shared.Setup.Database;
using GSM.Shared.Setup.Database.Service;
using GSM.Shared.Setup.Jwt.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GSM.Shared.Setup.API;

public abstract class DefaultWebApplication
{
    public static WebApplication Create(string[] args, Action<WebApplicationBuilder>? webappBuilder = null)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureJwtSecurity();
        
        builder.Services.AddAuthorization();

        builder.Services.ConfigureDatabase<DefaultDbContext>();

        builder.Services.AddMvc();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        if (webappBuilder != null)  webappBuilder.Invoke(builder);
        
        return builder.Build();
    }

    public static void Run(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        
        app.Run();

    }
}