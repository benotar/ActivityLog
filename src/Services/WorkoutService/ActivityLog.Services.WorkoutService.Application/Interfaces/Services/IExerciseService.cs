using ActivityLog.Services.WorkoutService.Application.Common;
using ActivityLog.Services.WorkoutService.Application.Models;

namespace ActivityLog.Services.WorkoutService.Application.Interfaces.Services;

public interface IExerciseService
{
    Task<Result<Guid>> CreateAsync(CreateExerciseRequest exercise);

    Task<Result<IEnumerable<ExerciseInfo>>> GetAllAsync();
}
