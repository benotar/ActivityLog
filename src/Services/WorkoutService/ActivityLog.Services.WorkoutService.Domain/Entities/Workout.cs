using ActivityLog.SharedKernel.Domain;

namespace ActivityLog.Services.WorkoutService.Domain.Entities;

public class Workout : AuditableEntity, ISoftDelete
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public int CaloriesBurned { get; set; }
    public bool IsDeleted { get; set; }
}
