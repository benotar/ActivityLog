namespace ActivityLog.Services.WorkoutService.Domain.ExerciseAggregate;

public sealed class ExerciseData: List<Exercise>
{
    public ExerciseData()
    {
        var workoutId = Guid.CreateVersion7();
        AddRange([
            CreateExercise(workoutId, "Push-ups", 15, "Strength"),
            CreateExercise(workoutId, "Pull-ups", 10, "Strength"),
            CreateExercise(workoutId, "Squats", 20, "Strength"),
            CreateExercise(workoutId, "Lunges", 15, "Strength"),
            CreateExercise(workoutId, "Plank", 60, "Strength"),
            CreateExercise(workoutId, "Jumping Jacks", 30, "Cardio"),
            CreateExercise(workoutId, "Burpees", 12, "Cardio"),
            CreateExercise(workoutId, "Mountain Climbers", 25, "Cardio"),
            CreateExercise(workoutId, "High Knees", 30, "Cardio"),
            CreateExercise(workoutId, "Bicycle Crunches", 20, "Strength")    
        ]);
    }
    
    private static Exercise CreateExercise(Guid workoutId, string name, int duration, string type)
    {
        return new Exercise(workoutId, name, duration, type);
    }
}
