using ActivityLog.SharedKernel.Domain;
using ActivityLog.SharedKernel.Extensions;
using ErrorOr;

namespace ActivityLog.Services.WorkoutService.Domain.Workout.Enums;

public class ExerciseType : ValueObject
{
    public string Value { get; }

    private ExerciseType(string value)
    {
        Value = value;
    }

    public static ErrorOr<ExerciseType> Create(string type)
    {
        return type.IsNullOrEmpty()
            ? Error.Validation("ExerciseType.Value", "Value is required")
            : new ExerciseType(type);
    }

    public static ExerciseType Strength => new(nameof(Strength));
    public static ExerciseType Cardio => new(nameof(Cardio));

    public bool IsStrength() => Value == Strength.Value;
    
    public bool IsCardio() => Value == Cardio.Value;
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
