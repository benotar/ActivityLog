using System.Collections.ObjectModel;
using ActivityLog.Services.WorkoutService.Domain.Workout.Entities;
using ActivityLog.SharedKernel.Domain;
using ErrorOr;

namespace ActivityLog.Services.WorkoutService.Domain.Workout;

public class Workout : AuditableEntity, IAggregateRoot
{
    private readonly List<Exercise> _exercises = [];
    public Guid UserId { get; private set; }
    public string Description { get; private set; }
    
    public ReadOnlyCollection<Exercise> Exercises => _exercises.AsReadOnly();

    private Workout(Guid userId, string description)
    {
        UserId = userId;
        Description = description;
    }

    public static Workout Create(Guid userId, string description)
    {
        return new Workout(userId, description);
    }

    public void AddExercise(Exercise exercise)
    {
        _exercises.Add(exercise);
    }
}
