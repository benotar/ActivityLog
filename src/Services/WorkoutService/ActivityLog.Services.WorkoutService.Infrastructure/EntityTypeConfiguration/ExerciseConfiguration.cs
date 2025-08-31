using ActivityLog.Services.WorkoutService.Domain.ExerciseAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityLog.Services.WorkoutService.Infrastructure.EntityTypeConfiguration;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.WorkoutId).IsRequired();

        builder.HasOne(x => x.Workout)
            .WithMany(w => w.Exercises)
            .HasForeignKey(x => x.WorkoutId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.OwnsOne(p => p.Type, e =>
        {
            e.Property(p => p.Value).IsRequired();
            e.Property(p => p.Value).HasMaxLength(25);
        });

        builder.HasQueryFilter(x => !x.Workout.IsDeleted);
    }
}
