using ApiMvno.Infra.CrossCutting.Jsc.Interfaces;
using ApiMvno.Infra.CrossCutting.Jsc.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.Jsc;

public static class JscIoC
{
    public static void AddJsc(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JscOptions>(configuration.GetSection(JscOptions.sectionKey));

        services.AddSingleton<IJscAuthService, JscAuthService>();

        services.AddScoped<IJscPackageService, JscPackageService>();
        services.AddScoped<IJscMsisdnService, JscMsisdnService>();
        services.AddScoped<IJscConsumptionService, JscConsumptionService>();

        services.AddScoped<IJscSimCardService, JscSimCardService>();
        services.AddScoped<IJscSubscriptionService, JscSubscriptionService>();
    }
}