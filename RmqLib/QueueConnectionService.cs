using RabbitMQ.Client;
using RmqLib.@interface;

namespace RmqLib
{
    public class QueueConnectionService : IServiceProvider, IQueueConnection
    {
        public IConnection Connection { get; }

        public QueueConnectionService(string connectionString)
        {
            ConnectionFactory factory = new()
            {
                Uri = new Uri(connectionString)
            };
            Connection = factory.CreateConnection();
        }

        public object? GetService(Type serviceType)
        {
            return Connection;
        }
    }
}