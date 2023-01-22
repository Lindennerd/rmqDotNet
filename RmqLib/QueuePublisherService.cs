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

            _connection = connection;
            this.queue = queue;
            _channel = _connection.Connection.CreateModel();

            if (exchange != null)
            {
                this.exchange = exchange;
                _channel.ExchangeDeclare(exchange.Name, exchange.Type, exchange.Durable, exchange.AutoDelete, exchange.Arguments);
            }
            _ = _channel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, queue.Arguments);
        }

        public object? GetService(Type serviceType)
        {
            return _channel;
        }

        public void EventPublish<MessageType>(MessageType message, string routingKey = "") where MessageType : IMessage
        {
            _channel.BasicPublish(
                exchange is null ? "" : exchange.Name,
                string.IsNullOrEmpty(routingKey) ? queue.Name : routingKey,
                null,
                EncodeMessage(message));
        }

        public void MessagePublish<MessageType>(MessageType message, string routingKey = "") where MessageType : IMessage
        {
            IBasicProperties basicProps = _channel.CreateBasicProperties();
            basicProps.CorrelationId = Guid.NewGuid().ToString();
            basicProps.ReplyTo = string.IsNullOrEmpty(routingKey) ? queue.Name : routingKey;
            _channel.BasicPublish(
                exchange is null ? "" : exchange.Name,
                string.IsNullOrEmpty(routingKey) ? queue.Name : routingKey,
                basicProps,
                EncodeMessage(message));
        }

        private static ReadOnlyMemory<byte> EncodeMessage<MessageType>(MessageType message) where MessageType : IMessage
        {
            string messageJson = JsonSerializer.Serialize(message);
            return new(Encoding.UTF8.GetBytes(messageJson));
        }
    }
}
