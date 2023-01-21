using RmqLib.@interface;

namespace task_master_api.Exchanges
{
    public class DefaultExchange : IExchange
    {
        public string Name => "";
        public string Type => "direct";
        public bool Durable => true;
        public bool AutoDelete => false;
        public IDictionary<string, object>? Arguments => null;
    }
}