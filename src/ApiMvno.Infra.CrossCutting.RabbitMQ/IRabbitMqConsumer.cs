using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ApiMvno.Infra.CrossCutting.RabbitMQ;

public interface IRabbitMqConsumer
{
    delegate void ConsumerEventHandler(object? sender, BasicDeliverEventArgs ea, string? message);

    #region Basic Params
    
    void BasicConsume(string queue, ConsumerEventHandler consumerEventHandler);
    void BasicConsume(string clientProvidedName, string queue, ConsumerEventHandler consumerEventHandler);
    void BasicConsume(IList<string> hostnames, string queue, ConsumerEventHandler consumerEventHandler);
    void BasicConsume(IList<string> hostnames, string clientProvidedName, string queue, ConsumerEventHandler consumerEventHandler);
    void BasicConsume(IList<AmqpTcpEndpoint> endpoints, string queue, ConsumerEventHandler consumerEventHandler);
    void BasicConsume(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName, string queue, ConsumerEventHandler consumerEventHandler);
    void BasicConsume(IEndpointResolver endpointResolver, string clientProvidedName, string queue, ConsumerEventHandler consumerEventHandler);

    #endregion

    #region Full Params

    void BasicConsume(string queue, ConsumerEventHandler consumerEventHandler, bool durable = true,
        bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null,
        uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false,
        bool autoAck = false);
    void BasicConsume(string clientProvidedName, string queue, ConsumerEventHandler consumerEventHandler, bool durable = true,
        bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null,
        uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false,
        bool autoAck = false);
    void BasicConsume(IList<string> hostnames, string queue, ConsumerEventHandler consumerEventHandler, bool durable = true,
        bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null,
        uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false,
        bool autoAck = false);
    void BasicConsume(IList<string> hostnames, string clientProvidedName, string queue, ConsumerEventHandler consumerEventHandler, bool durable = true,
        bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null,
        uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false,
        bool autoAck = false);
    void BasicConsume(IList<AmqpTcpEndpoint> endpoints, string queue, ConsumerEventHandler consumerEventHandler, bool durable = true,
        bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null,
        uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false,
        bool autoAck = false);
    void BasicConsume(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName, string queue, ConsumerEventHandler consumerEventHandler, bool durable = true,
        bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null,
        uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false,
        bool autoAck = false);
    void BasicConsume(IEndpointResolver endpointResolver, string clientProvidedName, string queue, ConsumerEventHandler consumerEventHandler, bool durable = true,
        bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null,
        uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false,
        bool autoAck = false);

    #endregion
}