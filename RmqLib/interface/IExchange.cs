namespace RmqLib.@interface
{
    public interface IExchange
    {
        string Name { get; }
        string Type { get; }
        bool Durable { get; }
        bool AutoDelete { get; }
        IDictionary<string, object> Arguments { get; }
    }
}