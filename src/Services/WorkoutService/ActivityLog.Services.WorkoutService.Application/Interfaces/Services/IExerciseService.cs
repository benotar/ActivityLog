using ActivityLog.Services.WorkoutService.Application.Models;
using ErrorOr;

namespace ActivityLog.Services.WorkoutService.Application.Interfaces.Services;

public interface IExerciseService
{
    Task<ErrorOr<Guid>> CreateAsync(ExerciseModel exercise);

    Task<IEnumerable<ExerciseModel>> GetAllAsync();
}
