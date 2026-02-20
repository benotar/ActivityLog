using ActivityLog.Chassis.Configuration;
using ActivityLog.Chassis.EF;
using ActivityLog.Constants.Aspire;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Infrastructure;
using ActivityLog.SharedKernel.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityLog.Services.WorkoutService.Infrastructure;

public static class DependencyInjection
{
    public static void AddPersistenceLayer(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString(Components.Postgres.Workout);
        
        if (connectionString!.IsNullOrEmpty())
        {
            throw new InvalidOperationException($"The {Components.Postgres.Identity} connection string is empty");
        }
        
        builder.AddPostgresDbContext<WorkoutDbContext>(
            connectionString!,
            app => app.Services.AddMigration<WorkoutDbContext, WorkoutDbContextSeed>());
        
        builder.Services.AddScoped<IWorkoutDbContext>(sp => sp.GetRequiredService<WorkoutDbContext>());
    }
}
