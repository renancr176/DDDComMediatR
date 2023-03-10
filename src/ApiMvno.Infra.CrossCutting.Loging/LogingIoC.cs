using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.Loging;

public static class LogingIoC
{
    public static void AddLoging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILogingService, LogingService>();
    }
}