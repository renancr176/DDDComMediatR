using ApiMvno.Application.AutoMapper;
using ApiMvno.Application.Commands;
using ApiMvno.Application.Events;
using ApiMvno.Application.Queries;
using ApiMvno.Application.Services;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Core.Options;
using ApiMvno.Domain.Validators;
using ApiMvno.Infra.Data.Contexts.IdentityDb;
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
        services.Configure<GeneralOptions>(configuration.GetSection(GeneralOptions.sectionKey));

        #endregion

        #region DbContexts

        services.AddIdentityDb(configuration);
        services.AddMvnoDb(configuration);

        #endregion

        services.AddValidators();
        services.AddCommands();
        services.AddEvents();
        services.AddQueries();

        #region Internal Services

        services.AddServices();

        #endregion

        #region External Services


        #endregion
    }
}