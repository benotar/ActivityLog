namespace ActivityLog.SharedKernel.Domain;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
}

public abstract class Entity<TId> : Entity
    where TId : IEquatable<TId>
{
    public new TId Id { get; set; } = default!;
}
