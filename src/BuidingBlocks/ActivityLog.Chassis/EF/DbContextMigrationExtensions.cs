using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ActivityLog.Chassis.EF;

public static class DbContextMigrationExtensions
{
    private const string ActivitySourceName = "DbMigrations";
    private static readonly ActivitySource ActivitySource = new ActivitySource(ActivitySourceName);

    public static IServiceCollection AddMigration<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddOpenTelemetry().WithTracing(tracing => tracing.AddSource(ActivitySourceName));
        
        return services.AddHostedService(sp => new MigrationHostedService<TContext>(sp));
    }

    private static async Task MigrateDbContextAsync<TContext>(this IServiceProvider serviceProvider,
        CancellationToken stoppingToken)
        where TContext : DbContext
    {
        var dbContextName = typeof(TContext).Name;

        using var scope = serviceProvider.CreateScope();
        
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

        using var activity = ActivitySource.StartActivity($"Migration operation {typeof(TContext).Name}");

        try
        {
            logger.LogInformation("Migrating database with associated context {DbContextName}", dbContextName);

            var strategy = dbContext?.Database.CreateExecutionStrategy();

            if (strategy is not null)
            {
                await strategy.ExecuteAsync(() => InvokeMigrations(dbContext, stoppingToken));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "An error occured while migrating the database used on context {DbContextName}",
                dbContextName);

            activity?.AddException(ex);

            throw new InvalidOperationException(
                $"Database migration failed for {dbContextName}. See inner exception for details", ex);
        }
    }

    private static async Task InvokeMigrations<TContext>(TContext dbContext, CancellationToken stoppingToken)
        where TContext : DbContext?
    {
        using var activity = ActivitySource.StartActivity($"Migrating {typeof(TContext).Name}");

        try
        {
            await dbContext?.Database.MigrateAsync(cancellationToken: stoppingToken)!;
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);

            throw;
        }
    }

    private sealed class MigrationHostedService<TContext>(IServiceProvider serviceProvider) : BackgroundService
        where TContext : DbContext
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return serviceProvider.MigrateDbContextAsync<TContext>(stoppingToken);
        }
    }
}
