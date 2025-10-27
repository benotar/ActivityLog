using ActivityLog.Chassis.EF;
using ActivityLog.Constants.Aspire;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Infrastructure;
using ActivityLog.SharedKernel.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityLog.Services.WorkoutService.Infrastructure;

public static class Extensions
{
    public static void AddPersistenceLayer(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetSection($"{Components.ConnectionStrings}:{Components.Postgres.Workout}");

        if (connectionString.Value!.IsNullOrEmpty())
        {
            throw new InvalidOperationException($"The {Components.Postgres.Workout} connection string is empty");
        }
        
        builder.Services.AddScoped<IWorkoutDbContext>(provider => provider.GetRequiredService<WorkoutDbContext>());

        builder.AddPostgresDbContext<WorkoutDbContext>(
            connectionString.Value!,
            app => app.Services.AddMigration<WorkoutDbContext, WorkoutDbContextSeed>());
    }
}
