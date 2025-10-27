using System.ComponentModel.DataAnnotations;
using ActivityLog.SharedKernel.Extensions;

namespace ActivityLog.Services.WorkoutService.Application.Models;

public sealed record WorkoutSetInfo(
    Guid Id,
    Guid WorkoutExerciseId,
    int Reps,
    double Weight,
    double Calories
);

public sealed record CreateWorkoutSetRequest : IValidatableObject
{
    public Guid WorkoutExerciseId { get; init; }
    [MinLength(1)] public int Reps { get; init; }
    [MinLength(0)] public double Weight { get; init; }
    [MinLength(1)] public double Calories { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (WorkoutExerciseId.IsEmpty())
        {
            yield return new ValidationResult(
                "The workout exercise id cannot be an empty GUID",
                [nameof(WorkoutExerciseId)]);
        }
    }
}
