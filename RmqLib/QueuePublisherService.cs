using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RmqLib.@interface;

namespace RmqLib
{
    public class QueuePublisherService<TQueue, TExchange> : IServiceProvider, IQueuePublisher<TQueue, TExchange> where TQueue : IQueue where TExchange : IExchange
    {
        private readonly IQueueConnection _connection;
        private readonly IModel _channel;
        private readonly IQueue queue;
        private readonly IExchange? exchange;

        public QueuePublisherService(IQueueConnection connection, IQueue queue, IExchange? exchange)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (exchange != null)
            {
                this.exchange = exchange;
            }

            _connection = connection;
            this.queue = queue;
            _channel = _connection.Connection.CreateModel();
        }

        public object? GetService(Type serviceType)
        {
            return _channel;
        }

        public void Publish<MessageType>(MessageType message, string routingKey = "") where MessageType : IMessage
        {
            string messageJson = JsonSerializer.Serialize(message);
            ReadOnlyMemory<byte> messageBody = new(Encoding.UTF8.GetBytes(messageJson));

            _ = _channel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, queue.Arguments);
            _channel.BasicPublish(exchange is null ? "" : exchange.Name, routingKey, null, messageBody);
        }
    }
}
