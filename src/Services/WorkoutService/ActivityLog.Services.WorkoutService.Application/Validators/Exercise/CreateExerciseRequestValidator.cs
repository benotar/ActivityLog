using ActivityLog.Constants.Core;
using ActivityLog.Services.WorkoutService.Application.Models;
using FluentValidation;

namespace ActivityLog.Services.WorkoutService.Application.Validators.Exercise;

public class CreateExerciseRequestValidator : AbstractValidator<CreateExerciseRequest>
{
    public CreateExerciseRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.MuscleGroup).NotEmpty();

        RuleFor(x => x.Equipment).MaximumLength(DataSchemaLength.Max);
    }
}
