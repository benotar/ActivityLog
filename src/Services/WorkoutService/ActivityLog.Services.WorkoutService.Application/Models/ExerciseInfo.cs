namespace ActivityLog.Services.WorkoutService.Application.Models;

public sealed record ExerciseInfo(
    Guid Id,
    string Name,
    string Equipment,
    string MuscleGroup);


public sealed class CreateExerciseRequest
{
    public string Name { get; set; }
    
    public string? Equipment { get; set; }
    
    public string MuscleGroup { get; set; }
}