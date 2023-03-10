using ApiMvno.Infra.CrossCutting.Portability.Interfaces;
using ApiMvno.Infra.CrossCutting.Portability.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.Portability;

public static class PortabilityIoC
{
    public static void AddPortability(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PortabilityOptions>(configuration.GetSection(PortabilityOptions.sectionKey));
        services.AddSingleton<IPortabilityAuthService, PortabilityAuthService>();
        
        services.AddScoped<IPortabilityService, PortabilityService>();
    }
}