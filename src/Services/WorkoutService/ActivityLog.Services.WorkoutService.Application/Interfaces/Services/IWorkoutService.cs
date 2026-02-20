using ActivityLog.Services.WorkoutService.Application.Models;
using ActivityLog.SharedKernel.Domain;

namespace ActivityLog.Services.WorkoutService.Application.Interfaces.Services;

public interface IWorkoutService
{
    Task<Result<Guid>> CreateAsync(CreateWorkoutRequest request,  CancellationToken cancellationToken = default);
    
    Task<Result<WorkoutInfo>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<Result<IEnumerable<WorkoutInfo>>> GetAllAsync(CancellationToken cancellationToken = default);
}
