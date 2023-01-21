using RmqLib.@interface;
using task_master_domain.task;

namespace task_master_api.Models.Task
{
    public class NewTaskModel : IMessage
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public task_master_domain.task.TaskStatus Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public TaskId TaskId { get; set; } = null!;
    }
}
