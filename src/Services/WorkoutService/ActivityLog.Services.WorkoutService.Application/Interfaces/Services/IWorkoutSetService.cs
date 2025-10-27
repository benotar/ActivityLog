using ActivityLog.Services.WorkoutService.Application.Models;
using ActivityLog.SharedKernel.Domain;

namespace ActivityLog.Services.WorkoutService.Application.Interfaces.Services;

public interface IWorkoutSetService
{
    Task<Result<Guid>> CreateAsync(CreateWorkoutSetRequest request, CancellationToken cancellationToken = default);
    
    Task<Result<WorkoutSetInfo>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<Result<IEnumerable<WorkoutSetInfo>>> GetAllAsync(CancellationToken cancellationToken = default);
}
