using ActivityLog.Chassis.EF;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Infrastructure;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Services;
using ActivityLog.Services.WorkoutService.Application.Models;
using ActivityLog.Services.WorkoutService.Domain.Entities;
using ActivityLog.SharedKernel.Domain;
using ActivityLog.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ActivityLog.Services.WorkoutService.Application.Services;

public class ExerciseService : IExerciseService
{
    private readonly IWorkoutDbContext _dbContext;

    public ExerciseService(IWorkoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid>>CreateAsync(CreateExerciseRequest exercise, CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Exercises.AnyAsync(x => x.Name == exercise.Name, cancellationToken: cancellationToken))
        {
            return ErrorCode.AlreadyExists;
        }
        
        var newExercise = new Exercise
        {
            Name = exercise.Name, MuscleGroup = exercise.MuscleGroup, Equipment = exercise.Equipment
        };

        _dbContext.Exercises.Add(newExercise);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return newExercise.Id;
    }

    public async Task<Result<Unit>> RemoveAsync(Guid id,CancellationToken cancellationToken = default)
    {
        var rowsAffected = await _dbContext.Exercises.Where(e => e.Id == id).ExecuteDeleteAsync(cancellationToken);

        return rowsAffected == 0 ? ErrorCode.NothingToDelete : new Unit();
    }

    public async Task<Result<ExerciseInfo>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var exercise = await _dbContext.Exercises.ByName(name).FirstOrDefaultAsync(cancellationToken);

        if (exercise == null)
        {
            return ErrorCode.NotFound;
        }

        return exercise.ToModel();
    }

    public async Task<Result<IEnumerable<ExerciseInfo>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var exercises = await _dbContext.Exercises
            .ToListAsync(cancellationToken: cancellationToken);

        return exercises.IsNotEmpty() ? exercises.Select(e => e.ToModel()).ToList() : [];
    }
}
