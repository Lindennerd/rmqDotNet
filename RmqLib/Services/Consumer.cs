using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RmqLib.Config;

namespace RmqLib.Services
{
    public class Consumer
    {
        private readonly IModel channel;
        private readonly RabbitMQConfig settings;
        private readonly ILogger<Consumer> logger;

        public Consumer(IModel channel, RabbitMQConfig settings, ILogger<Consumer> logger)
        {
            this.channel = channel;
            this.settings = settings;
            this.logger = logger;
        }

        public void ConsumeTopic<T>(string routingKey, Func<T, Task> handler) where T : class
        {
            channel.ExchangeDeclare(settings.ExchangeName, settings.ExchangeType, true);
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, settings.ExchangeName, routingKey);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (_, args) =>
            {
                logger.LogDebug("Received message from exchange {ExchangeName} with routing key {RoutingKey}", settings.ExchangeName, routingKey);
                logger.LogDebug("Message body: {MessageBody}", Encoding.UTF8.GetString(args.Body.ToArray()));

                string json = Encoding.UTF8.GetString(args.Body.ToArray());
                T message = JsonSerializer.Deserialize<T>(json);

                if (message == null) throw new Exception("Message is null");
                await handler(message);
                channel.BasicAck(args.DeliveryTag, false);
            };
            channel.BasicConsume(queueName, false, consumer);
            logger.LogTrace("Consuming messages from queue {QueueName}", queueName);
        }

        public void ConsumeDirect<T>(string queueName, Func<T, Task> handler) where T : class
        {
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, settings.ExchangeName, queueName);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (_, args) =>
            {
                logger.LogDebug("Received message from queue {QueueName}", queueName);
                logger.LogDebug("Message body: {MessageBody}", Encoding.UTF8.GetString(args.Body.ToArray()));

                string json = Encoding.UTF8.GetString(args.Body.ToArray());
                T message = JsonSerializer.Deserialize<T>(json);

                if (message == null) throw new Exception("Message is null");
                await handler(message);
                channel.BasicAck(args.DeliveryTag, false);
            };
            channel.BasicConsume(queueName, false, consumer);
            logger.LogTrace("Consuming messages from queue {QueueName}", queueName);
        }
    }
}