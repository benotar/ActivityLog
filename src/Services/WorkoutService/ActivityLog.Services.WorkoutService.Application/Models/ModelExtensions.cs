namespace ActivityLog.Services.WorkoutService.Application.Models;

public static class ModelExtensions
{
    public static ExerciseInfo ToModel(this Domain.Entities.Exercise exercise)
    {
        return new ExerciseInfo(exercise.Id, exercise.Name, exercise.Equipment, exercise.MuscleGroup);
    }
}
