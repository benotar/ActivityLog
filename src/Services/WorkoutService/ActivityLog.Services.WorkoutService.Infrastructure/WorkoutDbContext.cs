using ActivityLog.Services.WorkoutService.Domain.ExerciseAggregate;
using ActivityLog.Services.WorkoutService.Domain.WorkoutAggregate;
using Microsoft.EntityFrameworkCore;

namespace ActivityLog.Services.WorkoutService.Infrastructure;

public class WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : DbContext(options)
{
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Exercise> Exercises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkoutDbContext).Assembly);
    }
}
