using GSM.Services.Customers.Services.Queries;
using GSM.Services.Customers.Services.Queries.Interfaces;

namespace GSM.Services.Customers.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureQueryServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerQueryService, CustomerQueryService>();
    }
}