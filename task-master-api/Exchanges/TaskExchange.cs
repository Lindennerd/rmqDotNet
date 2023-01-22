using RmqLib.@interface;

namespace task_master_api.Exchanges
{
    public class TaskExchange : IExchange
    {
        public string Name => "task";
        public string Type => "topic";
        public bool Durable => true;
        public bool AutoDelete => false;
        public IDictionary<string, object>? Arguments => null;
    }
}