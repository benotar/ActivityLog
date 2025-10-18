using ActivityLog.Services.WorkoutService.Application.Configuration;
using ActivityLog.Services.WorkoutService.Application.Interfaces;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Services;
using ActivityLog.Services.WorkoutService.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityLog.Services.WorkoutService.Application;

public static class Extensions
{
    public static void AddApplicationLayer(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(DatabaseConfiguration.Key));

        services.AddScoped<IExerciseService, ExerciseService>();

        services.AddValidatorsFromAssembly(typeof(Extensions).Assembly, includeInternalTypes: true);
    }
}
