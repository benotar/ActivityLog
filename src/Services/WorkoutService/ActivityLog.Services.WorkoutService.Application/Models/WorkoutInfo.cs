using System.ComponentModel.DataAnnotations;
using ActivityLog.SharedKernel.Extensions;

namespace ActivityLog.Services.WorkoutService.Application.Models;

public sealed record WorkoutInfo(
    Guid Id,
    Guid UserId,
    string Notes,
    DateTime CreatedAt,
    DateTime? LastModifiedAt
);

public sealed record CreateWorkoutRequest : IValidatableObject
{
    public Guid UserId { get; init; }
    [Required] public string Notes { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (UserId.IsEmpty())
        {
            yield return new ValidationResult(
                "The user id cannot be an empty GUID",
                [nameof(UserId)]);
        }
    }
}
