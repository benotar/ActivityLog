using ActivityLog.SharedKernel.Domain;

namespace ActivityLog.Services.WorkoutService.Domain.Entities;

public class WorkoutExercise : Entity
{
    public Guid WorkoutId { get; set; }
    
    public Guid ExerciseId { get; set; }
    
    public Workout? Workout { get; set; }
    
    public Exercise? Exercise { get; set; }
    
    public ICollection<WorkoutSet> Sets { get; set; } = [];

    public double TotalCalories => Sets.Sum(s => s.Calories);
}
