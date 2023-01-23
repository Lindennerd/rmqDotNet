using TaskMaster.Domain.Common;
using TaskMaster.Domain.Task.ValueObjects;
using TaskMaster.Domain.Task.Entity;
using TaskMaster.Domain.Project.ValueObjects;

namespace TaskMaster.Domain.Task
{
    public sealed class Task : AggregateRoot<TaskId>
    {
        private Task(
            TaskId id,
            string title,
            string description,
            ProjectId project,
            DateTime dueDate,
            TaskPriority priority) : base(id)
        {
            Title = title;
            Description = description;
            Project = project;
            DueDate = dueDate;
            Priority = priority;
            Status = ValueObjects.TaskStatus.New;
        }

        public static Task Create(
            string title,
            string description,
            ProjectId project,
            DateTime dueDate,
            TaskPriority priority)
        {
            return new(TaskId.CreateUnique(), title, description, project, dueDate, priority);
        }

        private readonly List<string> Tags = new();
        private readonly List<Task> SubTasks = new();
        private readonly List<Comment> Comments = new();

        public string Title { get; }
        public string Description { get; }
        public DateTime DueDate { get; }
        public TaskPriority Priority { get; }
        public ValueObjects.TaskStatus Status { get; }
        public ProjectId Project { get; }

        public IReadOnlyCollection<string> GetTags => Tags.ToList();

        public IReadOnlyCollection<Task> GetSubTasks()
        {
            return SubTasks.ToList();
        }

        public IReadOnlyCollection<Comment> GetComments()
        {
            return Comments.ToList();
        }
    }
}