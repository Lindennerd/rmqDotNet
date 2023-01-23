using TaskMaster.Domain.Common;

namespace TaskMaster.Domain.Project.ValueObjects
{
    public sealed class ProjectId : ValueObject
    {
        public Guid Value { get; }

        private ProjectId(Guid value)
        {
            Value = value;
        }

        public static ProjectId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}