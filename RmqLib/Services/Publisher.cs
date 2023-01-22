using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RmqLib.Config;

namespace RmqLib.Services
{
    public class Publisher
    {
        private readonly IModel channel;
        private readonly RabbitMQConfig settings;
        private readonly ILogger<Publisher> logger;

        public Publisher(IModel channel, RabbitMQConfig settings, ILogger<Publisher> logger)
        {
            this.channel = channel;
            this.settings = settings;
            this.logger = logger;
        }

        public void PublishEvent<T>(T message, string routingKey) where T : class
        {
            string json = JsonSerializer.Serialize(message);
            ReadOnlyMemory<byte> body = new(Encoding.UTF8.GetBytes(json));
            channel.BasicPublish(settings.ExchangeName, routingKey, null, body);
            logger.LogTrace("Published message to exchange {ExchangeName} with routing key {RoutingKey}", settings.ExchangeName, routingKey);
        }

        public void PublishMessage<T>(T message, string queueName) where T : class
        {
            string json = JsonSerializer.Serialize(message);
            ReadOnlyMemory<byte> body = new(Encoding.UTF8.GetBytes(json));

            IBasicProperties basicProperties = channel.CreateBasicProperties();
            basicProperties.Persistent = true;
            basicProperties.CorrelationId = Guid.NewGuid().ToString();
            basicProperties.ReplyTo = queueName;

            channel.BasicPublish(settings.ExchangeName, queueName, null, body);
        }
    }
}