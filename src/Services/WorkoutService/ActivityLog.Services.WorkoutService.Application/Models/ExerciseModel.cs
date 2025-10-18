namespace ActivityLog.Services.WorkoutService.Application.Models.Exercise;

public sealed record ExerciseModel(
    Guid Id,
    string Name,
    string Equipment,
    string MuscleGroup);

