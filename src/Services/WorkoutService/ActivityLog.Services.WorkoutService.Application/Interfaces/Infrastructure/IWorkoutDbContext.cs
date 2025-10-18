using ActivityLog.Chassis.EF;
using ActivityLog.Services.WorkoutService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivityLog.Services.WorkoutService.Application.Interfaces.Infrastructure;

public interface IWorkoutDbContext : IDbContext
{
    DbSet<Workout> Workouts { get; set; }
    DbSet<Exercise> Exercises { get; set; }
    DbSet<WorkoutExercise> WorkoutExercises { get; set; }
    DbSet<WorkoutSet> WorkoutSets { get; set; }
}
