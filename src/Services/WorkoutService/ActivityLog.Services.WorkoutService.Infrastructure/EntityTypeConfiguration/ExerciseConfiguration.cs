using ActivityLog.Constants.Core;
using ActivityLog.Services.WorkoutService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityLog.Services.WorkoutService.Infrastructure.EntityTypeConfiguration;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("Exercises");
        
        builder.HasKey(ex => ex.Id);
        
        builder.Property(ex => ex.Name).IsRequired().HasMaxLength(DataSchemaLength.ExtraLarge);
        
        builder.Property(ex => ex.Name).HasMaxLength(DataSchemaLength.ExtraLarge);
        
        builder.Property(ex => ex.Name).HasMaxLength(DataSchemaLength.ExtraLarge);

        builder.HasMany(ex => ex.WorkoutExercises)
            .WithOne(we => we.Exercise)
            .HasForeignKey(we => we.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
