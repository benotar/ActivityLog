using ActivityLog.Services.WorkoutService.Application.Interfaces.Infrastructure;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Services;
using ActivityLog.Services.WorkoutService.Application.Models;
using ActivityLog.Services.WorkoutService.Domain.Entities;
using ActivityLog.SharedKernel.Domain;
using ActivityLog.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ActivityLog.Services.WorkoutService.Application.Services;

public class WorkoutSetService : IWorkoutSetService
{
    private readonly IWorkoutDbContext _dbContext;
    private readonly ILogger<WorkoutSetService> _logger;

    public WorkoutSetService(IWorkoutDbContext dbContext, ILogger<WorkoutSetService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid>> CreateAsync(CreateWorkoutSetRequest request, CancellationToken cancellationToken = default)
    {
        if (!await _dbContext.WorkoutSets.AnyAsync(item => item.WorkoutExerciseId == request.WorkoutExerciseId,
                cancellationToken))
        {
            _logger.LogWarning(
                "The element \"workout exercise\" with the specified id {WorkoutExerciseId} was not found in the database",
                request.WorkoutExerciseId);

            return ErrorCode.NotFound;
        }

        var newItem = new WorkoutSet
        {
            WorkoutExerciseId = request.WorkoutExerciseId,
            Reps = request.Reps,
            Weight = request.Weight,
            Calories = request.Calories
        };

        _dbContext.WorkoutSets.Add(newItem);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return newItem.Id;
    }

    public async Task<Result<WorkoutSetInfo>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await _dbContext.WorkoutSets.FindAsync([id], cancellationToken);

        if (item is not null)
        {
            return item.ToModel();
        }

        _logger.LogWarning(
            "The element \'workout set\' with the specified id {WorkoutSetId} was not found in the database",
            id);

        return ErrorCode.NotFound;
    }

    public async Task<Result<IEnumerable<WorkoutSetInfo>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _dbContext.WorkoutSets.ToListAsync(cancellationToken);

        return items.IsNotEmpty()
            ? Result<IEnumerable<WorkoutSetInfo>>.Success(items.ToModels())
            : ErrorCode.NotFound;
    }
}
