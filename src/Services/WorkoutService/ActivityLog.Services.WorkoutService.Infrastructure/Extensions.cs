using ActivityLog.Chassis.EF;
using ActivityLog.ServiceDefaults.Configuration;
using ActivityLog.Services.WorkoutService.Application.Configuration;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityLog.Services.WorkoutService.Infrastructure;

public static class Extensions
{
    public static void AddPersistenceLayer(this IHostApplicationBuilder builder)
    {
        var dbConfig = ConfigurationFactory
            .BindAndGet(builder.Configuration, DatabaseConfiguration.Key, () => new DatabaseConfiguration());

        var connectionString = string.Format(dbConfig.ConnectionString,
            dbConfig.User,
            dbConfig.Password,
            dbConfig.Name);

        builder.Services.AddScoped<IWorkoutDbContext>(provider => provider.GetRequiredService<WorkoutDbContext>());

        builder.AddPostgresDbContext<WorkoutDbContext>(
            connectionString,
            app => app.Services.AddMigration<WorkoutDbContext, WorkoutDbContextSeed>());
    }
}
