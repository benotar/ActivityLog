namespace ActivityLog.Services.WorkoutService.Domain.Entities;

public sealed class StrengthWorkout: Workout
{
    public int Reps { get; set; }
    public int Sets { get; set; }
    public double? Weight { get; set; }
}
