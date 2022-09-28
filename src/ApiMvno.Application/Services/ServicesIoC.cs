using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Application.Services;

public static class ServicesIoC
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}