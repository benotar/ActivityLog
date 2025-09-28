using ActivityLog.Constants.Core;
using ActivityLog.Services.WorkoutService.Domain.Entities;
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

        builder.Property(w => w.Notes).HasMaxLength(DataSchemaLength.ExtraLarge);
        
        builder.Property(w => w.CreatedAt).HasDefaultValueSql(DateTimeHelper.SqlUtcNow);

        builder.HasMany(w => w.WorkoutExercises)
            .WithOne(we => we.Workout)
            .HasForeignKey(we => we.WorkoutId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
