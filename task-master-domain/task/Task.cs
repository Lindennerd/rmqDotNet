namespace task_master_domain.task
{
    public class Task : TaskId
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public IEnumerable<string> Tags { get; set; } = null!;
    }
}