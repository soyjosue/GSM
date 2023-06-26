using GSM.Services.Products.Services.Queries;
using GSM.Services.Products.Services.Queries.Interfaces;

namespace GSM.Services.Products.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureQueryService(this IServiceCollection services)
    {
        services.AddScoped<IProductQueryService, ProductQueryService>();
    }
}