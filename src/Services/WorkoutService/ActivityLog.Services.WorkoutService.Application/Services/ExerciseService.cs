using ActivityLog.Services.WorkoutService.Application.Interfaces.Infrastructure;
using ActivityLog.Services.WorkoutService.Application.Interfaces.Services;
using ActivityLog.Services.WorkoutService.Application.Models;
using ActivityLog.Services.WorkoutService.Domain.Entities;
using ActivityLog.Services.WorkoutService.Domain.Enums;
using ActivityLog.SharedKernel.Extensions;
using ErrorOr;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ActivityLog.Services.WorkoutService.Application.Services;

public class ExerciseService : IExerciseService
{
    private readonly IWorkoutDbContext _dbContext;
    private readonly IValidator<ExerciseModel> _validator;

    public ExerciseService(IWorkoutDbContext dbContext, IValidator<ExerciseModel> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task<ErrorOr<Guid>> CreateAsync(ExerciseModel exercise)
    {
        var validationResult = _validator.Validate(exercise);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => Error.Validation(e.PropertyName, e.ErrorMessage))
                .ToList();

            return errors;
        }
        
        if (await _dbContext.Exercises.AnyAsync(ex => ex.Id.Equals(exercise.Id)))
        {
            return Error.Conflict($"Exercise.{nameof(ErrorCode.AlreadyExists)}", "An exercise with the specified id already exists");
        }
        
        var newExercise = new Exercise
        {
            Id = exercise.Id, Name = exercise.Name, MuscleGroup = exercise.MuscleGroup, Equipment = exercise.Equipment
        };

        _dbContext.Exercises.Add(newExercise);

        await _dbContext.SaveChangesAsync();

        return newExercise.Id;
    }

    public async Task<IEnumerable<ExerciseModel>> GetAllAsync()
    {
        var exercises = await _dbContext.Exercises
            .Include(e => e.WorkoutExercises)
            .ThenInclude(we => we.Sets)
            .ToListAsync();

        return exercises.IsNotEmpty() ? exercises.Select(e => e.ToModel()).ToList() : Enumerable.Empty<ExerciseModel>();
    }
}
