using ActivityLog.Services.WorkoutService.Application.Models;
using FluentValidation;

namespace ActivityLog.Services.WorkoutService.Application.Validators.Exercise;

public sealed class CreateExerciseValidator : AbstractValidator<ExerciseModel>
{
    public CreateExerciseValidator()
    {
        RuleFor(ex => ex.Id)
            .NotEmpty();
        
        RuleFor(ex => ex.Name)
            .NotEmpty();
        
        RuleFor(ex => ex.MuscleGroup)
            .NotEmpty();
    }
}
