using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ApiMvno.Infra.CrossCutting.RabbitMQ;

public class RabbitMq : IRabbitMq
{
    private readonly IOptions<RabbitMqOptions> _options;

    public RabbitMqOptions Config => _options.Value;

    public RabbitMq(IOptions<RabbitMqOptions> options)
    {
        _options = options;
    }

    private ConnectionFactory GetFactory()
    {
        return new ConnectionFactory()
        {
            HostName = Config.HostName,
            Port = Config.Port,
            UserName = Config.UserName,
            Password = Config.Password
        };
    }

    public IConnection CreateConnection()
    {
        var factory = GetFactory();

        return factory.CreateConnection();
    }

    public IConnection CreateConnection(string clientProvidedName)
    {
        var factory = GetFactory();

        return factory.CreateConnection(clientProvidedName);
    }

    public IConnection CreateConnection(IList<string> hostnames)
    {
        var factory = GetFactory();

        return factory.CreateConnection(hostnames);
    }

    public IConnection CreateConnection(IList<string> hostnames, string clientProvidedName)
    {
        var factory = GetFactory();

        return factory.CreateConnection(hostnames, clientProvidedName);
    }

    public IConnection CreateConnection(IList<AmqpTcpEndpoint> endpoints)
    {
        var factory = GetFactory();

        return factory.CreateConnection(endpoints);
    }

    public IConnection CreateConnection(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName)
    {
        var factory = GetFactory();

        return factory.CreateConnection(endpoints, clientProvidedName);
    }

    public IConnection CreateConnection(IEndpointResolver endpointResolver, string clientProvidedName)
    {
        var factory = GetFactory();

        return factory.CreateConnection(endpointResolver, clientProvidedName);
    }
}