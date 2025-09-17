using ActivityLog.SharedKernel.Domain;

namespace ActivityLog.Services.WorkoutService.Domain.Entities;

public class WorkoutSet : Entity
{
    public Guid WorkoutExerciseId { get; set; }

    public int Reps { get; set; }
    
    public double Weight { get; set; }
    
    public double Calories { get; set; }
    
    public WorkoutExercise? WorkoutExercise { get; set; }
}
