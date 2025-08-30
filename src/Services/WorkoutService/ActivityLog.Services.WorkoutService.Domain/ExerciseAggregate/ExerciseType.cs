using ActivityLog.Services.WorkoutService.Domain.Exceptions;
using ActivityLog.SharedKernel.Domain;

namespace ActivityLog.Services.WorkoutService.Domain.ExerciseAggregate;

public class ExerciseType : ValueObject
{
    public string Value { get; }

    private ExerciseType(string value) => Value = value;

    public static ExerciseType Strength => new(nameof(Strength));
    public static ExerciseType Cardio => new(nameof(Cardio));

    public static ExerciseType Create(string value) => value switch
    {
        nameof(Strength) => Strength,
        nameof(Cardio) => Cardio,
        _ => throw new WorkoutDomainException($"Invalid exercise type: {value}")
    };

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
