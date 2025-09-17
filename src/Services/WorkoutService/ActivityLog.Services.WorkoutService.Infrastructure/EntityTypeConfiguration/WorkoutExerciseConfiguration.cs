using ActivityLog.Services.WorkoutService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityLog.Services.WorkoutService.Infrastructure.EntityTypeConfiguration;

public class WorkoutExerciseConfiguration : IEntityTypeConfiguration<WorkoutExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
    {
        builder.ToTable("WorkoutExercises");
        
        builder.HasKey(we => we.Id);
        
        builder.Property(we => we.WorkoutId).IsRequired();
        
        builder.Property(we => we.ExerciseId).IsRequired();

        builder.HasMany(we => we.Sets)
            .WithOne(we => we.WorkoutExercise)
            .HasForeignKey(we => we.WorkoutExerciseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
