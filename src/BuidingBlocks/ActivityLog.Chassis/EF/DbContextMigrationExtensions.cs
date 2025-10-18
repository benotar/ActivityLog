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


    public static IServiceCollection AddMigration<TContext>(this IServiceCollection services,
        Func<TContext, IServiceProvider, Task> seeder)
        where TContext : IDbContext
    {
        services.AddOpenTelemetry().WithTracing(tracing => tracing.AddSource(ActivitySourceName));

        return services.AddHostedService(sp => new MigrationHostedService<TContext>(sp, seeder));
    }

    public static IServiceCollection AddMigration<TContext, TDbSeeder>(this IServiceCollection services)
        where TContext : IDbContext
        where TDbSeeder : class, IDbSeeder<TContext>
    {
        services.AddScoped<IDbSeeder<TContext>, TDbSeeder>();
        return services.AddMigration<TContext>((context, sp) => sp.GetRequiredService<IDbSeeder<TContext>>().SeedAsync(context));
    }

    private static async Task MigrateDbContextAsync<TContext>(this IServiceProvider services,
        Func<TContext, IServiceProvider, Task> seeder)
        where TContext : IDbContext
    {
        var dbContextName = typeof(TContext).Name;

        using var scope = services.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();

        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

        using var activity = ActivitySource.StartActivity($"Migration operation {typeof(TContext).Name}");

        try
        {
            logger.LogInformation("Migrating database with associated context {DbContextName}", dbContextName);

            var strategy = dbContext?.Database.CreateExecutionStrategy();

            if (strategy is not null)
            {
                await strategy.ExecuteAsync(() => InvokeSeeder(seeder!, dbContext, scope.ServiceProvider));
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

    private static async Task InvokeSeeder<TContext>(Func<TContext, IServiceProvider, Task> seeder,
        TContext dbContext,
        IServiceProvider services) where TContext : IDbContext?
    {
        using var activity = ActivitySource.StartActivity($"Migrating {typeof(TContext).Name}");

        try
        {
            await dbContext?.Database.MigrateAsync()!;
            await seeder(dbContext, services);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);

            throw;
        }
    }

    private sealed class MigrationHostedService<TContext>(
        IServiceProvider serviceProvider,
        Func<TContext, IServiceProvider, Task> seeder) : BackgroundService
        where TContext : IDbContext
    {
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return serviceProvider.MigrateDbContextAsync(seeder);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}

public interface IDbSeeder<in TContext>
    where TContext : IDbContext
{
    Task SeedAsync(TContext dbContext);
}
