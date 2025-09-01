namespace ActivityLog.SharedKernel.Domain;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
}
