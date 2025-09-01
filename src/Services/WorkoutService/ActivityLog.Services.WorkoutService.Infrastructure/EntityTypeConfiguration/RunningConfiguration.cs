using ActivityLog.Services.WorkoutService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityLog.Services.WorkoutService.Infrastructure.EntityTypeConfiguration;

public class RunningConfiguration : IEntityTypeConfiguration<RunningWorkout>
{
    public void Configure(EntityTypeBuilder<RunningWorkout> builder)
    {
        builder.Property(r => r.Distance).IsRequired();
        builder.Property(r => r.PaceSecondsPerKm).IsRequired();
        builder.Property(r => r.AverageHeartRate).IsRequired();
    }
}
