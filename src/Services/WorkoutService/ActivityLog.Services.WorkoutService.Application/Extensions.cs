using ActivityLog.Services.WorkoutService.Application.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityLog.Services.WorkoutService.Application;

public static class Extensions
{
    public static void AddApplication(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(DatabaseConfiguration.Key));
    }
}
