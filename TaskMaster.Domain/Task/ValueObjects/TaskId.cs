using TaskMaster.Domain.Common;

namespace TaskMaster.Domain.Task.ValueObjects
{
    public sealed class TaskId : ValueObject
    {
        public Guid Value { get; }

        private TaskId(Guid value)
        {
            Value = value;
        }

        public static TaskId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}