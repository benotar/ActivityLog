using System.ComponentModel.DataAnnotations;
using ActivityLog.SharedKernel.Extensions;

namespace ActivityLog.Services.WorkoutService.Application.Models;

public sealed record WorkoutExerciseInfo(
    Guid Id,
    Guid WorkoutId,
    Guid ExerciseId,
    double TotalCalories
);

public sealed record CreateWorkoutExerciseRequest : IValidatableObject
{
    public Guid WorkoutId { get; init; }
    public Guid ExerciseId { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext _)
    {
        if (WorkoutId.IsEmpty())
        {
            yield return new ValidationResult(
                "The workout id cannot be an empty GUID",
                [nameof(WorkoutId)]);
        }

        if (ExerciseId.IsEmpty())
        {
            yield return new ValidationResult(
                "The exercise id cannot be an empty GUID",
                [nameof(ExerciseId)]);
        }
    }
}
