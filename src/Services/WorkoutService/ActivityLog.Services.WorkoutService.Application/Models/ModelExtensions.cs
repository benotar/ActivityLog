using ActivityLog.Services.WorkoutService.Domain.Entities;

namespace ActivityLog.Services.WorkoutService.Application.Models;

public static class ModelExtensions
{
    // Exercise
    public static ExerciseInfo ToModel(this Exercise item)
    {
        return new ExerciseInfo(
            item.Id,
            item.Name,
            item.Equipment,
            item.MuscleGroup
        );
    }

    public static IEnumerable<ExerciseInfo> ToModels(this IEnumerable<Exercise> items)
    {
        return items.Select(item => item.ToModel()).ToList();
    }

    // WorkoutExercise
    public static WorkoutExerciseInfo ToModel(this WorkoutExercise item)
    {
        return new WorkoutExerciseInfo(
            item.Id,
            item.WorkoutId,
            item.ExerciseId,
            item.TotalCalories
        );
    }

    public static IEnumerable<WorkoutExerciseInfo> ToModels(this IEnumerable<WorkoutExercise> items)
    {
        return items.Select(item => item.ToModel()).ToList();
    }

    // Workout
    public static WorkoutInfo ToModel(this Workout item)
    {
        return new WorkoutInfo(
            item.Id,
            item.UserId,
            item.Notes,
            item.CreatedAt,
            item.LastModifiedAt
        );
    }

    public static IEnumerable<WorkoutInfo> ToModels(this IEnumerable<Workout> items)
    {
        return items.Select(item => item.ToModel()).ToList();
    }

    // WorkoutSet
    public static WorkoutSetInfo ToModel(this WorkoutSet item)
    {
        return new WorkoutSetInfo(
            item.Id,
            item.WorkoutExerciseId,
            item.Reps,
            item.Weight,
            item.Calories
        );
    }

    public static IEnumerable<WorkoutSetInfo> ToModels(this IEnumerable<WorkoutSet> items)
    {
        return items.Select(item => item.ToModel()).ToList();
    }
}
