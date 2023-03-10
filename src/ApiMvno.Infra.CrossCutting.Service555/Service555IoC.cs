using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.Service555;

public static class Service555IoC
{
    public static void AddService555(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Service555Option>(configuration.GetSection(Service555Option.sectionKey));
        services.AddScoped<IService555Service, Service555Service>();
    }
}