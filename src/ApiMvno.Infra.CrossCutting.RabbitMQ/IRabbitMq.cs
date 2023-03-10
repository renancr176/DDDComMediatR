using RabbitMQ.Client;

namespace ApiMvno.Infra.CrossCutting.RabbitMQ;

public interface IRabbitMq
{
    RabbitMqOptions Config { get; }
    IConnection CreateConnection();
    IConnection CreateConnection(string clientProvidedName);
    IConnection CreateConnection(IList<string> hostnames);
    IConnection CreateConnection(IList<string> hostnames, string clientProvidedName);
    IConnection CreateConnection(IList<AmqpTcpEndpoint> endpoints);
    IConnection CreateConnection(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName);
    IConnection CreateConnection(IEndpointResolver endpointResolver, string clientProvidedName);
}