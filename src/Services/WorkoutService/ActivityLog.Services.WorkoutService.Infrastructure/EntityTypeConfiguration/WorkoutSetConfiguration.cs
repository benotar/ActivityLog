using ActivityLog.Services.WorkoutService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityLog.Services.WorkoutService.Infrastructure.EntityTypeConfiguration;

public class WorkoutSetConfiguration : IEntityTypeConfiguration<WorkoutSet>
{
    public void Configure(EntityTypeBuilder<WorkoutSet> builder)
    {
        builder.ToTable("WorkoutSets");
        
        builder.HasKey(ws => ws.Id);

        builder.Property(ws => ws.WorkoutExerciseId).IsRequired();
    }
}
