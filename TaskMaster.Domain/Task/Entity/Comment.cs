using TaskMaster.Domain.Common;
using TaskMaster.Domain.Task.ValueObjects;
using TaskMaster.Domain.User.ValueObjects;

namespace TaskMaster.Domain.Task.Entity
{
    public sealed class Comment : Entity<CommentId>
    {
        private Comment(CommentId id, string body, TaskId task, UserId author, CommentId replyTo) : base(id)
        {
            Body = body;
            TaskId = task;
            Author = author;
            ReplyTo = replyTo;
            CreatedAt = DateTime.UtcNow;
        }

        public static Comment Create(string body, TaskId task, UserId author, CommentId replyTo)
        {
            return new(CommentId.CreateUnique(), body, task, author, replyTo);
        }

        public string Body { get; }
        public DateTime CreatedAt { get; }
        public TaskId TaskId { get; }
        public UserId Author { get; }
        public CommentId? ReplyTo { get; }
    }
}