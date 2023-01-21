namespace RmqLib.@interface
{
    public interface IQueuePublisher<TQueue, TExchange>
    {
        void Publish<MessageType>(MessageType message, string routingKey = "") where MessageType : IMessage;
    }
}