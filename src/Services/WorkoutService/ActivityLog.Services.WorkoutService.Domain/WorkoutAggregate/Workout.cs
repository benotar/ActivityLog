using System.Collections.ObjectModel;
using ActivityLog.Services.WorkoutService.Domain.Exceptions;
using ActivityLog.Services.WorkoutService.Domain.ExerciseAggregate;
using ActivityLog.SharedKernel.Domain;
using ActivityLog.SharedKernel.Extensions;

namespace ActivityLog.Services.WorkoutService.Domain.WorkoutAggregate;

public class Workout : AuditableEntity, IAggregateRoot, ISoftDelete
{
    private readonly List<Exercise> _exercises = [];
    public Guid UserId { get; private set; }
    public string Description { get; private set; }

    public ReadOnlyCollection<Exercise> Exercises => _exercises.AsReadOnly();

    public Workout(Guid userId, string description)
    {
        Description = description.IsNullOrWhiteSpace()
            ? throw new WorkoutDomainException("Workout description is required")
            : description;

        UserId = userId;
    }
    
    public Workout Update(string description)
    {
        //var isChanged = string.Compare(Description, description, StringComparison.OrdinalIgnoreCase) != 0; TODO for future domain events

        Description = description.IsNullOrWhiteSpace()
            ? throw new WorkoutDomainException("Workout description is required")
            : description;

        return this;
    }

    public bool IsDeleted { get; set; }

    public void Delete()
    {
        IsDeleted = true;
    }
}
