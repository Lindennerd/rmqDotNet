namespace task_master_domain.task
{
    public class Comment : AbstractEntity
    {
        public string Body { get; set; } = null!;
        public Comment RepliesTo { get; set; } = null!;
        public Task Task { get; set; } = null!;
    }
}