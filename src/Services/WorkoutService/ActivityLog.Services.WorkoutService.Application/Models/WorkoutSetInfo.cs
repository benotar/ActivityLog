using System.ComponentModel.DataAnnotations;

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
    [MinLength(1)] int Reps,
    [MinLength(0)] double Weight,
    [MinLength(1)] double Calories
);
