using ActivityLog.Services.WorkoutService.Application.Models;
using FluentValidation;

namespace ActivityLog.Services.WorkoutService.Application.Validators.Exercise;

public class CreateExerciseRequestValidator : AbstractValidator<CreateExerciseRequest>
{
    public CreateExerciseRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255)
            .WithMessage("The exercise name is required");

        RuleFor(x => x.MuscleGroup)
            .NotEmpty()
            .MaximumLength(255)
            .WithMessage("The exercise muscle group is required");

        RuleFor(x => x.Equipment)
            .MaximumLength(255);
    }
}
