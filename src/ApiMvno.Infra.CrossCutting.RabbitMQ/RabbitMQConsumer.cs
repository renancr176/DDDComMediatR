
using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ApiMvno.Infra.CrossCutting.RabbitMQ;

public class RabbitMqConsumer : IRabbitMqConsumer
{
    private readonly ILogger<RabbitMqConsumer> _logger;
    private readonly IRabbitMq _rabbitMq;

    public RabbitMqConsumer(ILogger<RabbitMqConsumer> logger, IRabbitMq rabbitMq)
    {
        _logger = logger;
        _rabbitMq = rabbitMq;
    }

    private void BasicConsume(IConnection connection, string queue,
        IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler, bool durable = true, bool exclusive = false,
        bool autoDelete = false, IDictionary<string, object> arguments = null, uint prefetchSize = 0,
        ushort prefetchCount = 1, bool global = false, bool autoAck = false)
    {
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue,
                durable,
                exclusive,
                autoDelete,
                arguments);

            channel.BasicQos(prefetchSize, prefetchCount, global);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                consumerEventHandler(sender, ea, message);

                int dots = message.Split('.').Length - 1;
                Thread.Sleep(dots * 1000);

                // Note: it is possible to access the channel via
                //       ((EventingBasicConsumer)sender).Model here
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue,
                autoAck,
                consumer: consumer);

            Console.ReadLine();
        }
    }

    #region Basic Params

    public void BasicConsume(string queue, IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler)
    {
        BasicConsume(queue, consumerEventHandler, false);
    }

    public void BasicConsume(string clientProvidedName, string queue, IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler)
    {
        BasicConsume(clientProvidedName, queue, consumerEventHandler, false);
    }

    public void BasicConsume(IList<string> hostnames, string queue, IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler)
    {
        BasicConsume(hostnames, queue, consumerEventHandler, false);
    }

    public void BasicConsume(IList<string> hostnames, string clientProvidedName, string queue, IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler)
    {
        BasicConsume(hostnames, clientProvidedName, queue, consumerEventHandler, false);
    }

    public void BasicConsume(IList<AmqpTcpEndpoint> endpoints, string queue, IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler)
    {
        BasicConsume(endpoints, queue, consumerEventHandler, false);
    }

    public void BasicConsume(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName, string queue, IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler)
    {
        BasicConsume(endpoints, clientProvidedName, queue, consumerEventHandler, false);
    }

    public void BasicConsume(IEndpointResolver endpointResolver, string clientProvidedName, string queue,
        IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler)
    {
        BasicConsume(endpointResolver, clientProvidedName, queue, consumerEventHandler, false);
    }

    #endregion

    #region Full Params

    public void BasicConsume(string queue, IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler, bool durable = true,
        bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null,
        uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false,
        bool autoAck = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection();
            BasicConsume(connection, queue, consumerEventHandler, durable, exclusive, autoDelete, arguments,
                prefetchSize, prefetchCount, global, autoAck);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    public void BasicConsume(string clientProvidedName, string queue, IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler,
        bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null,
        uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false, bool autoAck = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection(clientProvidedName);
            BasicConsume(connection, queue, consumerEventHandler, durable, exclusive, autoDelete, arguments,
                prefetchSize, prefetchCount, global, autoAck);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    public void BasicConsume(IList<string> hostnames, string queue, IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler, bool durable = true,
        bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null, uint prefetchSize = 0,
        ushort prefetchCount = 1, bool global = false, bool autoAck = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection(hostnames);
            BasicConsume(connection, queue, consumerEventHandler, durable, exclusive, autoDelete, arguments,
                prefetchSize, prefetchCount, global, autoAck);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    public void BasicConsume(IList<string> hostnames, string clientProvidedName, string queue, IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler,
        bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null,
        uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false, bool autoAck = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection(hostnames, clientProvidedName);
            BasicConsume(connection, queue, consumerEventHandler, durable, exclusive, autoDelete, arguments,
                prefetchSize, prefetchCount, global, autoAck);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    public void BasicConsume(IList<AmqpTcpEndpoint> endpoints, string queue, IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler, bool durable = true,
        bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null, uint prefetchSize = 0,
        ushort prefetchCount = 1, bool global = false, bool autoAck = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection(endpoints);
            BasicConsume(connection, queue, consumerEventHandler, durable, exclusive, autoDelete, arguments,
                prefetchSize, prefetchCount, global, autoAck);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    public void BasicConsume(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName, string queue, IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler,
        bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null,
        uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false, bool autoAck = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection(endpoints, clientProvidedName);
            BasicConsume(connection, queue, consumerEventHandler, durable, exclusive, autoDelete, arguments,
                prefetchSize, prefetchCount, global, autoAck);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    public void BasicConsume(IEndpointResolver endpointResolver, string clientProvidedName, string queue,
        IRabbitMqConsumer.ConsumerEventHandler consumerEventHandler, bool durable = true, bool exclusive = false, bool autoDelete = false,
        IDictionary<string, object> arguments = null, uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false,
        bool autoAck = false)
    {
        try
        {
            using var connection = _rabbitMq.CreateConnection(endpointResolver, clientProvidedName);
            BasicConsume(connection, queue, consumerEventHandler, durable, exclusive, autoDelete, arguments,
                prefetchSize, prefetchCount, global, autoAck);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message, e.StackTrace);
            throw;
        }
    }

    #endregion
}