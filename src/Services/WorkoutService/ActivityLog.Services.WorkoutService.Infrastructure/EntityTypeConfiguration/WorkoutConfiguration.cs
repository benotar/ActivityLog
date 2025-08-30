using ActivityLog.Services.WorkoutService.Domain.WorkoutAggregate;
using ActivityLog.SharedKernel.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityLog.Services.WorkoutService.Infrastructure.EntityTypeConfiguration;

public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.UserId).IsRequired();
        
        builder.Property(w => w.Description).HasMaxLength(500);

        builder.Property(w => w.CreatedAt).HasDefaultValueSql(DateTimeHelper.SqlUtcNow);

        builder.Navigation(w => w.Exercises).AutoInclude();
    }
}
