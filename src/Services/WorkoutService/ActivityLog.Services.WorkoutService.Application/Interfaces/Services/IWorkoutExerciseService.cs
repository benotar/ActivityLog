using ActivityLog.Services.WorkoutService.Application.Models;
using ActivityLog.SharedKernel.Domain;

namespace ActivityLog.Services.WorkoutService.Application.Interfaces.Services;

public interface IWorkoutExerciseService
{
    Task<Result<Guid>> CreateAsync(CreateWorkoutExerciseRequest request, CancellationToken cancellationToken = default);
    
    Task<Result<WorkoutExerciseInfo>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<Result<IEnumerable<WorkoutExerciseInfo>>> GetAllAsync(CancellationToken cancellationToken = default);
}
