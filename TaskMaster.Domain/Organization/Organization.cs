using TaskMaster.Domain.Common;
using TaskMaster.Domain.Organization.ValueObjects;
using TaskMaster.Domain.Project.ValueObjects;
using TaskMaster.Domain.User.ValueObjects;

namespace TaskMaster.Domain.Organization
{
    public sealed class Organization : AggregateRoot<OrganizationId>
    {
        private Organization(OrganizationId id, string name, string description, string avatar, string avatarUrl) : base(id)
        {
            Name = name;
            Description = description;
            Avatar = avatar;
            AvatarUrl = avatarUrl;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;
            Url = $"https://taskmaster.com/organizations/{id.Value}";
        }

        public static Organization Create(string name, string description, string avatar, string avatarUrl)
        {
            return new(OrganizationId.CreateUnique(), name, description, avatar, avatarUrl);
        }

        private readonly List<UserId> Members = new();
        private readonly List<UserId> Administrators = new();

        private readonly List<ProjectId> Projects = new();

        public string Name { get; }
        public string Description { get; }
        public string Avatar { get; }
        public string AvatarUrl { get; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; }
        public string Url { get; }

        public IReadOnlyCollection<UserId> GetMembers()
        {
            return Members.ToList();
        }

        public IReadOnlyCollection<UserId> GetAdministrators()
        {
            return Administrators.ToList();
        }

        public IReadOnlyCollection<ProjectId> GetProjects()
        {
            return Projects.ToList();
        }
    }
}