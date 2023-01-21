using RabbitMQ.Client;

namespace RmqLib.@interface
{
    public interface IQueueConnection
    {
        IConnection Connection { get; }
    }
}