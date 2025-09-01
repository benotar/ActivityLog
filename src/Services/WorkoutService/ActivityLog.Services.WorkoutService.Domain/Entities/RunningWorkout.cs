namespace ActivityLog.Services.WorkoutService.Domain.Entities;

public sealed class RunningWorkout : Workout
{
    public double Distance { get; set; }
    public int AverageHeartRate { get; set; }
    public double PaceSecondsPerKm { get; set; }
}
