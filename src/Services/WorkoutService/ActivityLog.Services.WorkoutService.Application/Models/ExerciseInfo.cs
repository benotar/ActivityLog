namespace ActivityLog.Services.WorkoutService.Application.Models;

public sealed record ExerciseInfo(
    Guid Id,
    string Name,
    string Equipment,
    string MuscleGroup);


public sealed record CreateExerciseRequest(
    string Name, 
    string? Equipment, 
    string MuscleGroup);