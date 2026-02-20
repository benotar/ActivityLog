using ActivityLog.SharedKernel.Domain;

namespace ActivityLog.Services.WorkoutService.Domain.Entities;

public class Exercise : Entity, IName
{
    public string Name { get; set; }
    
    public string? Equipment { get; set; }
    
    public string MuscleGroup { get; set; }
    
    public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = [];
}
