namespace RmqLib.@interface
{
    public interface IQueuePublisher<TQueue, TExchange>
    {
        void EventPublish<MessageType>(MessageType message, string routingKey = "") where MessageType : IMessage;
        void MessagePublish<MessageType>(MessageType message, string routingKey = "") where MessageType : IMessage;
    }
}