namespace RmqLib.@interface
{
    public interface IQueue
    {
        string Name { get; }
        bool Durable { get; }
        bool Exclusive { get; }
        bool AutoDelete { get; }
        IDictionary<string, object>? Arguments { get; }
    }
}