using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Application.Events;

public static class EventIoC
{
    public static void AddEvents(this IServiceCollection services)
    {
        //services.AddScoped<INotificationHandler<CustomEvent>, CustomEventHandler>();
    }
}