using ActivityLog.Chassis.Configuration;
using ActivityLog.Chassis.EF;
using ActivityLog.Services.WorkoutService.Application.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityLog.Services.WorkoutService.Infrastructure;

public static class Extensions
{
    public static void AddPersistence(this IHostApplicationBuilder builder)
    {
        var dbConfig = ConfigurationFactory
            .BindAndGet(builder.Configuration, DatabaseConfiguration.Key, () => new DatabaseConfiguration());

        builder.Services.AddDbContext<WorkoutDbContext>(options =>
            options.UseNpgsql(string.Format(dbConfig.ConnectionString, dbConfig.User, dbConfig.Password,
                dbConfig.Host, dbConfig.DbName)));

        builder.Services.AddMigration<WorkoutDbContext>();
    }
}
