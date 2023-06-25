using GSM.Services.Security.Services.Queries;
using GSM.Services.Security.Services.Queries.Interfaces;

namespace GSM.Services.Security.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureQueryServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountQueryService, AccountQueryService>();
    }
}