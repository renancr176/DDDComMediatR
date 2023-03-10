using ApiMvno.Infra.CrossCutting.BackServiceApi.Interfaces;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi;

public static class BackServiceIoC
{
    public static void AddBackService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BackServiceOptions>(configuration.GetSection(BackServiceOptions.sectionKey));
        services.AddSingleton<IBackServiceAuthService, BackServiceAuthService>();

        services.AddScoped<IBackServiceCompanyService, BackServiceCompanyService>();
        services.AddScoped<IBackServiceCustomerService, BackServiceCustomerService>();
        services.AddScoped<IBackServiceAccountService, BackServiceAccountService>();
        services.AddScoped<IBackServicePreSubscriptionService, BackServicePreSubscriptionService>();
        services.AddScoped<IBackServiceSalesforceService, BackServiceSalesforceService>();
    }
}