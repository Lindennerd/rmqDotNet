using TaskMaster.Domain.Common;
using TaskMaster.Domain.Task.ValueObjects;
using TaskMaster.Domain.User.ValueObjects;

namespace TaskMaster.Domain.User
{
    public sealed class User : AggregateRoot<UserId>
    {
        private User(UserId id, string name, string email, string avatar, string avatarUrl) : base(id)
        {
            Name = name;
            Email = email;
            Avatar = avatar;
            AvatarUrl = avatarUrl;
        }

        public static User Create(string name, string email, string avatar, string avatarUrl)
        {
            return new(UserId.CreateUnique(), name, email, avatar, avatarUrl);
        }
        private readonly List<TaskId> Tasks = new();

        public string Name { get; }
        public string Email { get; }
        public string Avatar { get; }
        public string AvatarUrl { get; }

        public IReadOnlyCollection<TaskId> GetTasks()
        {
            return Tasks.ToList();
        }
    }
}