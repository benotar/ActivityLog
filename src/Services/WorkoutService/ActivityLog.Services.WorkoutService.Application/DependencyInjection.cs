using ActivityLog.Services.WorkoutService.Application.Configuration;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Services;
using ActivityLog.Services.WorkoutService.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityLog.Services.WorkoutService.Application;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(DatabaseConfiguration.Key));
        
        services.AddScoped<IExerciseService, ExerciseService>();

        services.AddScoped<IWorkoutExerciseService, WorkoutExerciseService>();

        services.AddScoped<IWorkoutService, Services.WorkoutService>();

        services.AddScoped<IWorkoutSetService, WorkoutSetService>();
    }
}
