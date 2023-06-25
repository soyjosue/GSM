using System.Net.NetworkInformation;
using Microsoft.Extensions.DependencyInjection;

namespace GSM.Shared.Setup.CQRS;

public static class MediatorExtensions
{
    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
    }
}