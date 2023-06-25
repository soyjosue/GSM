using GSM.Shared.EnvironmentVariable;
using GSM.Shared.EnvironmentVariable.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GSM.Shared.Setup.Database.Service;

public static class DatabaseConfigureService
{
    public static void ConfigureDatabase<T>(this IServiceCollection services, string? connectionStringPath = null) where T : DbContext
    {
        string connectionString = EnvironmentUtils.Get<string>(string.IsNullOrEmpty(connectionStringPath)
            ? DatabaseEnvironmentVariableEnum.DefaultConnectionString
            : connectionStringPath)!;
        
        services.AddDbContext<T>(
            options => options.UseSqlServer(connectionString)
        );
    }
}