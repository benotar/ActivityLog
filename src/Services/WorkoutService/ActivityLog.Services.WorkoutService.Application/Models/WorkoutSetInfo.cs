namespace ActivityLog.Services.WorkoutService.Application.Models;

public sealed record WorkoutSetInfo(
    Guid Id,
    Guid WorkoutExerciseId,
    int Reps,
    double Weight,
    double Calories
);

public sealed record CreateWorkoutSetRequest(
    Guid WorkoutExerciseId,
    int Reps,
    double Weight,
    double Calories
);
