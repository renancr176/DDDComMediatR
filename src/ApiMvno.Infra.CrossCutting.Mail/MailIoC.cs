using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.Mail;

public static class MailIoC
{
    public static void AddMail(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailApiOptions>(configuration.GetSection(MailApiOptions.sectionKey));
        services.AddScoped<IMailService, MailService>();
    }
}