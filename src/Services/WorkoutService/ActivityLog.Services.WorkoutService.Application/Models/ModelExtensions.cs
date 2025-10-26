using ActivityLog.Services.WorkoutService.Domain.Entities;

namespace ActivityLog.Services.WorkoutService.Application.Models;

public static class ModelExtensions
{
    public static ExerciseInfo ToModel(this Exercise item)
    {
        return new ExerciseInfo(item.Id, item.Name, item.Equipment, item.MuscleGroup);
    }

    public static IEnumerable<ExerciseInfo> ToModels(this IEnumerable<Exercise> items)
    {
        return items.Select(item => item.ToModel()).ToList();
    }

    public static WorkoutExerciseInfo ToModel(this WorkoutExercise item)
    {
        return new WorkoutExerciseInfo(item.Id, item.WorkoutId, item.ExerciseId, item.TotalCalories);
    }

    public static IEnumerable<WorkoutExerciseInfo> ToModels(this IEnumerable<WorkoutExercise> items)
    {
        return items.Select(item => item.ToModel()).ToList();
    }
}
