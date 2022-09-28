using ApiMvno.Application.AutoMapper;
using ApiMvno.Application.Commands;
using ApiMvno.Application.Events;
using ApiMvno.Application.Services;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Core.Options;
using ApiMvno.Domain.Interfaces.Validators;
using ApiMvno.Domain.Validators;
using ApiMvno.Infra.CrossCutting.Mail;
using ApiMvno.Infra.CrossCutting.Sms;
using ApiMvno.Infra.Data.Contexts.MvnoDb;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.IoC;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        services.AddAutoMapperProfiles();

        #region Options

        services.Configure<JwtTokenOptions>(configuration.GetSection(JwtTokenOptions.sectionKey));
        services.Configure<MailApiOptions>(configuration.GetSection(MailApiOptions.sectionKey));
        services.Configure<SmsApiOptions>(configuration.GetSection(SmsApiOptions.sectionKey));
        services.Configure<GeneralOptions>(configuration.GetSection(GeneralOptions.sectionKey));

        #endregion

        #region DbContexts

        services.AddMvnoDb(configuration);

        #endregion

        #region Validators

        services.AddScoped<ICompanyValidator, CompanyValidator>();

        #endregion

        #region Commands
        services.AddCommands();
        #endregion

        #region Events
        services.AddEvents();
        #endregion

        #region Internal Services

        services.AddServices();

        #endregion

        #region External Services

        services.AddScoped<IMailService, MailService>();
        services.AddScoped<ISmsService, SmsService>();

        #endregion
    }
}