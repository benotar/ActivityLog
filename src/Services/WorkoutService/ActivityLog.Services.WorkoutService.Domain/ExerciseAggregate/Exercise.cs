using ActivityLog.Services.WorkoutService.Domain.Exceptions;
using ActivityLog.Services.WorkoutService.Domain.WorkoutAggregate;
using ActivityLog.SharedKernel.Domain;
using ActivityLog.SharedKernel.Extensions;

namespace ActivityLog.Services.WorkoutService.Domain.ExerciseAggregate;

public sealed class Exercise : AuditableEntity
{
    public Guid WorkoutId { get; private set; }
    public Workout Workout { get; private set; }
    public string Name { get; private set; }
    public ExerciseType Type { get; private set; }
    public int Duration { get; private set; }
    
    public Exercise(Guid workoutId, string name, int duration, string type)
    {
        WorkoutId = workoutId.IsEmpty()
            ? throw new WorkoutDomainException("WorkoutId is required")
            : workoutId;
        
        Name = name.IsNullOrWhiteSpace()
            ? throw new WorkoutDomainException("Exercise name is required")
            : name;

        Duration = duration <= 0
            ? throw new WorkoutDomainException("Duration must be greater than 0 seconds")
            : duration;
        
        Type = ExerciseType.Create(type);
    }

    public Exercise Update(string name, int duration, string type)
    {
        var updatedType = ExerciseType.Create(type);
        
        //  TODO for future domain events
        // var isChanged = string.Compare(Name, name, StringComparison.OrdinalIgnoreCase) != 0 ||
        //                 string.Compare(Type.Value, updatedType.Value, StringComparison.OrdinalIgnoreCase) != 0 ||
        //                 Repetitions.Value == repetitions;
        
        Name = name.IsNullOrWhiteSpace()
            ? throw new WorkoutDomainException("Exercise name is required")
            : name;
        
        Duration = duration <= 0
            ? throw new WorkoutDomainException("Duration must be greater than 0 seconds")
            : duration;
        
        Type = updatedType;

        return this;
    }
}
