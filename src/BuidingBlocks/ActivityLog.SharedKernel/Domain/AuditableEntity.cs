using ActivityLog.SharedKernel.Helpers;

namespace ActivityLog.SharedKernel.Domain;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAt { get; init; } = DateTimeHelper.UtcNow();
    public DateTime? LastModifiedAt { get; set; }
}
