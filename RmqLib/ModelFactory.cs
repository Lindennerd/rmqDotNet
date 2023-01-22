using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RmqLib.Config;

namespace RmqLib
{
    public class ModelFactory : IDisposable
    {
        private readonly ILogger<ModelFactory> logger;
        private readonly RabbitMQConfig settings;
        private readonly IConnection connection;

        public ModelFactory(IConnectionFactory connectionFactory, RabbitMQConfig settings, ILogger<ModelFactory> logger)
        {
            this.logger = logger;
            this.settings = settings;
            connection = connectionFactory.CreateConnection();
        }

        public IModel CreateChannel()
        {
            IModel channel = connection.CreateModel();
            channel.ExchangeDeclare(settings.ExchangeName, settings.ExchangeType, true, false, null);
            logger.LogTrace("Created channel for exchange {ExchangeName}", settings.ExchangeName);
            return channel;
        }

        public void Dispose()
        {
            connection.Dispose();
            GC.SuppressFinalize(this);
            logger.LogTrace("Disposed channel for exchange {ExchangeName}", settings.ExchangeName);
        }
    }
}