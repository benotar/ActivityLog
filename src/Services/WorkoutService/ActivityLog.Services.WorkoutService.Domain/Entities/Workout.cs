using ActivityLog.SharedKernel.Domain;

namespace ActivityLog.Services.WorkoutService.Domain.Entities;

public class Workout : AuditableEntity
{ 
    public Guid UserId { get; set; }
    
    public string Notes { get; set; }
    
    public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = [];

    public double TotalCalories => WorkoutExercises.Sum(we => we.TotalCalories);
}
