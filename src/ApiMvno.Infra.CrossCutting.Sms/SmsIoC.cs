using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.Sms;

public static class SmsIoC
{
    public static void AddSms(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmsApiOptions>(configuration.GetSection(SmsApiOptions.sectionKey));
        services.AddSingleton<ISmsService, SmsService>();
    }
}