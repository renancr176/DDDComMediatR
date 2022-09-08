using ApiMvno.Application.Commands.CompanyCommands;
using ApiMvno.Application.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Application.Events;

public static class EventIoC
{
    public static void AddEvents(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler<CompanyCreatedEvent>, CompanyCreatedEventHandler>();
    }
}