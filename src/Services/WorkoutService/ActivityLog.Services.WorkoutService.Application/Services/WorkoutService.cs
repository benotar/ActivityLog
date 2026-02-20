using ActivityLog.Services.WorkoutService.Application.Interfaces.Infrastructure;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Services;
using ActivityLog.Services.WorkoutService.Application.Models;
using ActivityLog.Services.WorkoutService.Domain.Entities;
using ActivityLog.SharedKernel.Domain;
using ActivityLog.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ActivityLog.Services.WorkoutService.Application.Services;

public class WorkoutService : IWorkoutService
{
    private readonly IWorkoutDbContext _dbContext;
    private readonly ILogger<WorkoutService> _logger;

    public WorkoutService(IWorkoutDbContext dbContext, ILogger<WorkoutService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid>> CreateAsync(CreateWorkoutRequest request, CancellationToken cancellationToken = default)
    {
        var newItem = new Workout { UserId = request.UserId, Notes = request.Notes };

        _dbContext.Workouts.Add(newItem);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return newItem.Id;
    }

    public async Task<Result<WorkoutInfo>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await _dbContext.Workouts.FindAsync([id], cancellationToken);

        if (item is not null)
        {
            return item.ToModel();
        }

        _logger.LogWarning("The workout with the specified id {WorkoutId} was not found in the database", id);

        return ErrorCode.NotFound;
    }

    public async Task<Result<IEnumerable<WorkoutInfo>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _dbContext.Workouts.ToListAsync(cancellationToken);

        return items.IsNotEmpty()
            ? Result<IEnumerable<WorkoutInfo>>.Success(items.ToModels())
            : ErrorCode.NotFound;
    }
}
