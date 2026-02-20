using ActivityLog.Chassis.EF;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Infrastructure;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Services;
using ActivityLog.Services.WorkoutService.Application.Models;
using ActivityLog.Services.WorkoutService.Domain.Entities;
using ActivityLog.SharedKernel.Domain;
using ActivityLog.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ActivityLog.Services.WorkoutService.Application.Services;

public class ExerciseService : IExerciseService
{
    private readonly IWorkoutDbContext _dbContext;
    private readonly ILogger<ExerciseService> _logger;

    public ExerciseService(IWorkoutDbContext dbContext, ILogger<ExerciseService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid>> CreateAsync(CreateExerciseRequest request, CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Exercises.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken))
        {
            _logger.LogWarning("The exercise with name '{ExerciseName}' not found in the database", request.Name);
            return ErrorCode.AlreadyExists;
        }

        var newItem = new Exercise { Name = request.Name, MuscleGroup = request.MuscleGroup, Equipment = request.Equipment };

        _dbContext.Exercises.Add(newItem);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return newItem.Id;
    }

    public async Task<Result<Unit>> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var rowsAffected = await _dbContext.Exercises.Where(e => e.Id == id).ExecuteDeleteAsync(cancellationToken);

        return rowsAffected == 0 ? ErrorCode.NothingToDelete : new Unit();
    }

    public async Task<Result<ExerciseInfo>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var item = await _dbContext.Exercises.ByName(name).FirstOrDefaultAsync(cancellationToken);

        if (item is not null)
        {
            return item.ToModel();
        }

        _logger.LogWarning("The exercise with name \'{ExerciseName}\' was not found in the database", name);

        return ErrorCode.NotFound;
    }

    public async Task<Result<IEnumerable<ExerciseInfo>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _dbContext.Exercises.ToListAsync(cancellationToken);

        return items.IsNotEmpty()
            ? Result<IEnumerable<ExerciseInfo>>.Success(items.ToModels())
            : ErrorCode.NotFound;
    }
}
