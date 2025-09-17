using ActivityLog.Services.WorkoutService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivityLog.Services.WorkoutService.Infrastructure;

public class WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : DbContext(options)
{
    public DbSet<Workout> Workouts { get; set; }
    
    public DbSet<Exercise> Exercises { get; set; }
    
    public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
    
    public DbSet<WorkoutSet> WorkoutSets { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkoutDbContext).Assembly);
    }
}
