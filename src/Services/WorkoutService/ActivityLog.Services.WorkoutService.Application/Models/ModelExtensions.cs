using ActivityLog.Services.WorkoutService.Domain.Entities;

namespace ActivityLog.Services.WorkoutService.Application.Models;

public static class ModelExtensions
{
    public static ExerciseModel ToModel(this Exercise exercise)
    {
        return new ExerciseModel(exercise.Id, exercise.Name, exercise.Equipment, exercise.MuscleGroup);
    }
}
