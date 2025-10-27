using ActivityLog.Services.WorkoutService.Application.Models;
using FluentValidation;

namespace ActivityLog.Services.WorkoutService.Application.Validators.WorkoutSet;

public class CreateWorkoutSetRequestValidator : AbstractValidator<CreateWorkoutSetRequest>
{
    public CreateWorkoutSetRequestValidator()
    {
        RuleFor(x => x.WorkoutExerciseId).NotEmpty();

        RuleFor(x => x.Reps).GreaterThan(0);

        RuleFor(x => x.Calories).GreaterThan(0);

        RuleFor(x => x.Weight).GreaterThan(0);
    }
}
