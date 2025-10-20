using ActivityLog.Services.WorkoutService.Application.Common;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Infrastructure;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Services;
using ActivityLog.Services.WorkoutService.Application.Models;
using ActivityLog.Services.WorkoutService.Domain.Entities;
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

    public async Task<Result<Guid>>CreateAsync(CreateExerciseRequest exercise)
    {
        if (await _dbContext.Exercises.AnyAsync(x => x.Name == exercise.Name))
        {
            return $"The exercise \'{exercise.Name}\' already exists in the database";
        }
        
        var newExercise = new Exercise
        {
            Name = exercise.Name, MuscleGroup = exercise.MuscleGroup, Equipment = exercise.Equipment
        };

        _dbContext.Exercises.Add(newExercise);

        await _dbContext.SaveChangesAsync();

        return newExercise.Id;
    }

    public async Task<Result<IEnumerable<ExerciseInfo>>> GetAllAsync()
    {
        var exercises = await _dbContext.Exercises
            .ToListAsync();

        return exercises.IsNotEmpty() ? exercises.Select(e => e.ToModel()).ToList() : [];
    }
}
