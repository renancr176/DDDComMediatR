using RabbitMQ.Client;

namespace ApiMvno.Infra.CrossCutting.RabbitMQ;

public interface IRabbitMqPublisher
{
    #region Basic Params

    void BasicPublish(string queue, string message, bool persistent = true);
    void BasicPublish(string clientProvidedName, string queue, string message, bool persistent = true);
    void BasicPublish(IList<string> hostnames, string queue, string message, bool persistent = true);
    void BasicPublish(IList<string> hostnames, string clientProvidedName, string queue, string message, bool persistent = true);
    void BasicPublish(IList<AmqpTcpEndpoint> endpoints, string queue, string message, bool persistent = true);
    void BasicPublish(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName, string queue, string message, bool persistent = true);
    void BasicPublish(IEndpointResolver endpointResolver, string clientProvidedName, string queue, string message, bool persistent = true);

    #endregion

    #region Full Params

    void BasicPublish(string queue, string message, string exchange, string routingKey,
        IBasicProperties basicProperties = null, bool durable = true, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, bool persistent = true);
    void BasicPublish(string clientProvidedName, string queue, string message, string exchange, string routingKey,
        IBasicProperties basicProperties = null, bool durable = true, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, bool persistent = true);

    void BasicPublish(IList<string> hostname, string queue, string message, string exchange, string routingKey,
        IBasicProperties basicProperties = null, bool durable = true, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, bool persistent = true);
    void BasicPublish(IList<string> hostnames, string clientProvidedName, string queue, string message, string exchange, string routingKey,
        IBasicProperties basicProperties = null, bool durable = true, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, bool persistent = true);
    void BasicPublish(IList<AmqpTcpEndpoint> endpoints, string queue, string message, string exchange, string routingKey,
        IBasicProperties basicProperties = null, bool durable = true, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, bool persistent = true);
    void BasicPublish(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName, string queue, string message, string exchange, string routingKey,
        IBasicProperties basicProperties = null, bool durable = true, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, bool persistent = true);
    void BasicPublish(IEndpointResolver endpointResolver, string clientProvidedName, string queue, string message, string exchange, string routingKey,
        IBasicProperties basicProperties = null, bool durable = true, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, bool persistent = true);

    #endregion
}