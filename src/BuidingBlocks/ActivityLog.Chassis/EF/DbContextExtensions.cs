using ActivityLog.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityLog.Chassis.EF;

public static class DbContextExtensions
{
    public static void AddPostgresDbContext<TDbContext>(
        this IHostApplicationBuilder builder,
        string connectionString,
        Action<IHostApplicationBuilder>? action = null,
        bool excludeDefaultInterceptors = false
    ) where TDbContext : DbContext
    {
        var services = builder.Services;

        if (!excludeDefaultInterceptors)
        {
            services.AddScoped<DbCommandInterceptor, QueryPerformanceInterceptor>();
        }

        services.AddDbContext<TDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString);

            var interceptors = sp.GetServices<IInterceptor>().ToArray();

            if (interceptors.IsNotEmpty())
            {
                options.AddInterceptors(interceptors);
            }
        });

        action?.Invoke(builder);
    }
}
