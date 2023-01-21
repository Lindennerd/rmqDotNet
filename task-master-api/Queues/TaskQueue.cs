using RmqLib.@interface;
namespace task_master_api.Queues
{
    public class TaskQueue : IQueue
    {
        public string Name { get; } = "task_queue";
        public bool Durable { get; } = true;
        public bool Exclusive { get; }

        public bool AutoDelete { get; }

        public IDictionary<string, object>? Arguments { get; }
    }
}