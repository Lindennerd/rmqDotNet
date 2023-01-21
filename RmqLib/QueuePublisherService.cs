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

        public void EventPublish<MessageType>(MessageType message, string routingKey = "") where MessageType : IMessage
        {
            _ = _channel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, queue.Arguments);
            _channel.BasicPublish(exchange is null ? "" : exchange.Name, queue.Name, null, EncodeMessage(message));
        }

        public void MessagePublish<MessageType>(MessageType message, string routingKey = "") where MessageType : IMessage
        {
            _ = _channel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, queue.Arguments);
            IBasicProperties basicProps = _channel.CreateBasicProperties();
            basicProps.CorrelationId = Guid.NewGuid().ToString();
            basicProps.ReplyTo = queue.Name;
            _channel.BasicPublish(exchange is null ? "" : exchange.Name, queue.Name, basicProps, EncodeMessage(message));
        }

        private static ReadOnlyMemory<byte> EncodeMessage<MessageType>(MessageType message) where MessageType : IMessage
        {
            string messageJson = JsonSerializer.Serialize(message);
            return new(Encoding.UTF8.GetBytes(messageJson));
        }
    }
}
