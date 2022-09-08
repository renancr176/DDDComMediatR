using ApiMvno.Application.AutoMapper;
using ApiMvno.Application.Commands;
using ApiMvno.Application.Events;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Interfaces.Validators;
using ApiMvno.Domain.Validators;
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

        #region Services

        #endregion
    }
}