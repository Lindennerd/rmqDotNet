using TaskMaster.Domain.Common;
using TaskMaster.Domain.Organization.ValueObjects;
using TaskMaster.Domain.Project.ValueObjects;
using TaskMaster.Domain.Task.ValueObjects;
using TaskMaster.Domain.User.ValueObjects;

namespace TaskMaster.Domain.Project
{
    public sealed class Project : AggregateRoot<ProjectId>
    {
        private Project(
            ProjectId id,
            string name,
            string description,
            UserId creator, OrganizationId organization) : base(id)
        {
            Name = name;
            Description = description;
            Creator = creator;
            Organization = organization;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;
        }

        public static Project Create(
            string name,
            string description,
            UserId creator,
            OrganizationId organization)
        {
            return new(
                ProjectId.CreateUnique(),
                name,
                description,
                creator, organization);
        }

        private readonly List<TaskId> Tasks = new();

        public string Name { get; }
        public string Description { get; }
        public UserId Creator { get; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; }
        public OrganizationId Organization { get; }

        public IReadOnlyCollection<TaskId> GetTasks()
        {
            return Tasks.ToList();
        }
    }
}