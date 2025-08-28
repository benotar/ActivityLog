using ActivityLog.Services.WorkoutService.Domain.Workout.Enums;
using ActivityLog.Services.WorkoutService.Domain.Workout.ValueObjects;
using ActivityLog.SharedKernel.Domain;
using ActivityLog.SharedKernel.Extensions;
using ErrorOr;

namespace ActivityLog.Services.WorkoutService.Domain.Workout.Entities;

public sealed class Exercise : Entity
{
    public string Name { get; private set; }
    public ExerciseType Type { get; private set; }
    public Repetition Repetitions { get; private set; }

    private Exercise(string name, Repetition repetitions, ExerciseType type)
    {
        Name = name;
        Repetitions = repetitions;
        Type = type;
    }

    public static ErrorOr<Exercise> Create(string name, int repetitions, string type)
    {
        if (type.IsNullOrEmpty())
        {
            return Error.Validation("Exercise.Name", "Name is required");
        }
        
        var createRepetitions = Repetition.Create(repetitions);

        if (createRepetitions.IsError)
        {
            return createRepetitions.Errors;
        }

        var createExerciseType = ExerciseType.Create(type);

        if (createExerciseType.IsError)
        {
            return createExerciseType.Errors;
        }

        return new Exercise(name, createRepetitions.Value, createExerciseType.Value);
    }
}
