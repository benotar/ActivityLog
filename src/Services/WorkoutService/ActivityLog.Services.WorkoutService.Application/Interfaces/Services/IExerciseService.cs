using ActivityLog.Services.WorkoutService.Application.Models;
using ActivityLog.SharedKernel.Domain;

namespace ActivityLog.Services.WorkoutService.Application.Interfaces.Services;

public interface IExerciseService
{
    Task<Result<Guid>> CreateAsync(CreateExerciseRequest request,CancellationToken cancellationToken = default);

    Task<Result<Unit>> RemoveAsync(Guid id,CancellationToken cancellationToken = default);
    
    Task<Result<ExerciseInfo>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    
    Task<Result<IEnumerable<ExerciseInfo>>> GetAllAsync(CancellationToken cancellationToken = default);
}
