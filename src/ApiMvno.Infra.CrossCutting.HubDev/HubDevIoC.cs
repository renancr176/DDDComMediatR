using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.HubDev;

public static class HubDevIoC
{
    public static void AddHubDev(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HubDevOptions>(configuration.GetSection(HubDevOptions.sectionKey));
        services.AddScoped<IHubDevService, HubDevService>();
    }
}