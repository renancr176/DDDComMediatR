using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.RabbitMQ;

public static class RabbitMQIoC
{
    public static void AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.sectionKey));
        services.AddScoped<IRabbitMq, RabbitMq>();
        services.AddScoped<IRabbitMqPublisher, RabbitMqPublisher>();
        services.AddScoped<IRabbitMqConsumer, RabbitMqConsumer>();
    }
}