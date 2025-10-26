using ActivityLog.Services.WorkoutService.Application.Interfaces.Infrastructure;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Services;
using ActivityLog.Services.WorkoutService.Application.Models;
using ActivityLog.Services.WorkoutService.Domain.Entities;
using ActivityLog.SharedKernel.Domain;
using ActivityLog.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ActivityLog.Services.WorkoutService.Application.Services;

public class WorkoutExerciseService : IWorkoutExerciseService
{
    private readonly IWorkoutDbContext _dbContext;
    private readonly ILogger<WorkoutExerciseService> _logger;

    public WorkoutExerciseService(IWorkoutDbContext dbContext, ILogger<WorkoutExerciseService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid>> CreateAsync(CreateWorkoutExerciseRequest request,
        CancellationToken cancellationToken = default)
    {
        if (await _dbContext.WorkoutExercises.AnyAsync(
                we => we.WorkoutId == request.WorkoutId && we.ExerciseId == request.ExerciseId, cancellationToken))
        {
            _logger.LogWarning(
                "The element \'workout exercise\' for exercise {ExerciseId} and workout {WorkoutId} was not found in the database",
                request.ExerciseId,
                request.WorkoutId);

            return ErrorCode.NotFound;
        }

        var newItem = new WorkoutExercise { ExerciseId = request.ExerciseId, WorkoutId = request.WorkoutId, };

        _dbContext.WorkoutExercises.Add(newItem);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return newItem.Id;
    }

    public async Task<Result<WorkoutExerciseInfo>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await _dbContext.WorkoutExercises.FindAsync([id], cancellationToken);

        if (item is not null)
        {
            return item.ToModel();
        }

        _logger.LogWarning("The element \'workout exercise\' with the id {WorkoutExerciseId} was not found in the database",
            id);

        return ErrorCode.NotFound;
    }

    public async Task<Result<IEnumerable<WorkoutExerciseInfo>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _dbContext.WorkoutExercises.ToListAsync(cancellationToken);

        return items.IsNotEmpty()
            ? Result<IEnumerable<WorkoutExerciseInfo>>.Success(items.ToModels())
            : ErrorCode.NotFound;
    }
}
