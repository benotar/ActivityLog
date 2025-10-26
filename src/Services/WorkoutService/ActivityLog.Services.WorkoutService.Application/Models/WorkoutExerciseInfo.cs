namespace ActivityLog.Services.WorkoutService.Application.Models;

public sealed record WorkoutExerciseInfo(
    Guid Id,
    Guid WorkoutId,
    Guid ExerciseId,
    double TotalCalories
);

public sealed record CreateWorkoutExerciseRequest(
    Guid WorkoutId,
    Guid ExerciseId
);
