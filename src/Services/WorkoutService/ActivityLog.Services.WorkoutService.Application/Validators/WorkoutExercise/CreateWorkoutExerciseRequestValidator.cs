using System.Data;
using ActivityLog.Services.WorkoutService.Application.Models;
using FluentValidation;

namespace ActivityLog.Services.WorkoutService.Application.Validators.WorkoutExercise;

public class CreateWorkoutExerciseRequestValidator : AbstractValidator<CreateWorkoutExerciseRequest>
{
    public CreateWorkoutExerciseRequestValidator()
    {
        RuleFor(x => x.WorkoutId).NotEmpty();
        
        RuleFor(x => x.ExerciseId).NotEmpty();
    }
}
