using Microsoft.Extensions.DependencyInjection;

namespace GSM.Shared.Setup.AutoMapper;

public static class AutoMapperExtensions
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
        => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}