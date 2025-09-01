using ActivityLog.Services.WorkoutService.Domain.Entities;
using ActivityLog.Services.WorkoutService.Domain.Enums;
using ActivityLog.SharedKernel.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityLog.Services.WorkoutService.Infrastructure.EntityTypeConfiguration;

public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.ToTable("Workouts");
        
        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.UserId).IsRequired();

        builder.Property(w => w.Name).HasMaxLength(500).IsRequired();

        builder.Property(w => w.CreatedAt).HasDefaultValueSql(DateTimeHelper.SqlUtcNow);
        
        builder.HasQueryFilter(w => !w.IsDeleted);

        builder.HasDiscriminator<string>(nameof(WorkoutType))
            .HasValue<StrengthWorkout>(nameof(WorkoutType.Strength))
            .HasValue<RunningWorkout>(nameof(WorkoutType.Running));
    }
}
