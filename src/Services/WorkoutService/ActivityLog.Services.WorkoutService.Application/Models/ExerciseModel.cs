namespace ActivityLog.Services.WorkoutService.Application.Models;

public sealed record ExerciseModel(
    Guid Id,
    string Name,
    string Equipment,
    string MuscleGroup);
