namespace TaskMaster.Domain.Common
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
        where TId : notnull
    {
        public TId Id { get; protected set; } = default!;

        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            Entity<TId> entity = (Entity<TId>)obj;

            return Id.Equals(entity.Id);
        }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !Equals(a, b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }
    }
}