using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.Log;

public static class LogIoC
{
    public static void AddLog(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILog, Log>();
    }
}