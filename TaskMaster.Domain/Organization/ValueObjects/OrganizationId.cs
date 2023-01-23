using TaskMaster.Domain.Common;

namespace TaskMaster.Domain.Organization.ValueObjects
{
    public sealed class OrganizationId : ValueObject
    {
        public Guid Value { get; }

        private OrganizationId(Guid value)
        {
            Value = value;
        }

        public static OrganizationId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}