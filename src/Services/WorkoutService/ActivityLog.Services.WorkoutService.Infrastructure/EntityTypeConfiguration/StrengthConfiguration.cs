using ActivityLog.Services.WorkoutService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityLog.Services.WorkoutService.Infrastructure.EntityTypeConfiguration;

public class StrengthConfiguration : IEntityTypeConfiguration<StrengthWorkout>
{
    public void Configure(EntityTypeBuilder<StrengthWorkout> builder)
    {
        builder.Property(s => s.Sets).IsRequired();
        builder.Property(s => s.Reps).IsRequired();
    }
}
