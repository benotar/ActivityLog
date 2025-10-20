using ActivityLog.Chassis.EF;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Infrastructure;
using ActivityLog.Services.WorkoutService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ActivityLog.Services.WorkoutService.Infrastructure;

public class WorkoutDbContextSeed : IDbSeeder<IWorkoutDbContext>
{
    private readonly ILogger<WorkoutDbContextSeed> _logger;

    public WorkoutDbContextSeed(ILogger<WorkoutDbContextSeed> logger)
    {
        _logger = logger;
    }

    public async Task SeedAsync(IWorkoutDbContext dbContext)
    {
        var benchPress = CreateExercise("Bench press", "Chest");
        var squat = CreateExercise("Squat", "Legs", "Barbell");
        if (!await dbContext.Exercises.AnyAsync())
        {
            _logger.LogInformation("<---- Seeding exercises ---->");
            dbContext.Exercises.AddRange(benchPress, squat);
            await dbContext.SaveChangesAsync();
        }

        var userId = Guid.CreateVersion7();
        var workout = CreateWorkout(userId, "Full Body Workout");
        if (!await dbContext.Workouts.AnyAsync())
        {
            _logger.LogInformation("<---- Seeding workouts ---->");
            dbContext.Workouts.Add(workout);
            await dbContext.SaveChangesAsync();
        }

        var benchPressWe = CreateWorkoutExercise(workout.Id, benchPress.Id);
        var squatWe = CreateWorkoutExercise(workout.Id, squat.Id);
        if (!await dbContext.WorkoutExercises.AnyAsync())
        {
            _logger.LogInformation("<---- Seeding workout-exercises ---->");
            dbContext.WorkoutExercises.AddRange(benchPressWe, squatWe);
            await dbContext.SaveChangesAsync();
        }

        var benchPressWeSetOne = CreateWorkoutSet(benchPressWe.Id, 10, 85, 65);
        var benchPressWeSetTwo = CreateWorkoutSet(benchPressWe.Id, 15, 80, 55);
        var squatWeSetTwo = CreateWorkoutSet(squatWe.Id, 15, 70, 35);

        if (!await dbContext.WorkoutSets.AnyAsync())
        {
            _logger.LogInformation("<---- Seeding workout-sets ---->");
            dbContext.WorkoutSets.AddRange(benchPressWeSetOne, benchPressWeSetTwo, squatWeSetTwo);
            await dbContext.SaveChangesAsync();
        }
    }


    private static Workout CreateWorkout(Guid userId, string notes)
    {
        return new Workout { UserId = userId, Notes = notes };
    }
    
    private static Exercise CreateExercise(string name, string muscleGroup, string equipment = null)

    {
        return new Exercise { Name = name, MuscleGroup = muscleGroup, Equipment = equipment };
    }

    private static WorkoutExercise CreateWorkoutExercise(Guid workoutId, Guid exerciseId)
    {
        return new WorkoutExercise { WorkoutId = workoutId, ExerciseId = exerciseId };
    }

    private static WorkoutSet CreateWorkoutSet(Guid workoutExerciseId, int reps, double weight, double calories)
    {
        return new WorkoutSet { WorkoutExerciseId = workoutExerciseId, Reps = reps, Weight = weight, Calories = calories };
    }
}

