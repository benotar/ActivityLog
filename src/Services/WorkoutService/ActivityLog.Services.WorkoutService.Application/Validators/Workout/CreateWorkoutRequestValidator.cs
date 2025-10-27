using ActivityLog.Services.WorkoutService.Application.Models;
using FluentValidation;

namespace ActivityLog.Services.WorkoutService.Application.Validators.Workout;

public class CreateWorkoutRequestValidator : AbstractValidator<CreateWorkoutRequest>
{
    public CreateWorkoutRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();

        RuleFor(x => x.Notes).NotEmpty();
    }
}
