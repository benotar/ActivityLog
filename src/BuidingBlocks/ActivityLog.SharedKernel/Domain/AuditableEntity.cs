using ActivityLog.SharedKernel.Helpers;

namespace ActivityLog.SharedKernel.Domain;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAt { get; init; } = DateTimeHelper.UtcNow();
    public DateTime? LastModifiedAt { get; set; }
}

public abstract class AuditableEntity<TId> : Entity<TId>
    where TId : IEquatable<TId>
{
    public DateTime CreatedAt { get; init; } = DateTimeHelper.UtcNow();
    public DateTime? LastModifiedAt { get; set; }
}
