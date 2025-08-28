using ActivityLog.SharedKernel.Domain;
using ErrorOr;

namespace ActivityLog.Services.WorkoutService.Domain.Workout.ValueObjects;

public class Repetition : ValueObject
{
    public int Value { get; }

    private Repetition(int value)
    {
        Value = value;
    }

    public static ErrorOr<Repetition> Create(int repetitions)
    {
        return repetitions < 1
            ? Error.Validation("Repetition.Value", "Value must be greater than zero")
            : new Repetition(repetitions);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
