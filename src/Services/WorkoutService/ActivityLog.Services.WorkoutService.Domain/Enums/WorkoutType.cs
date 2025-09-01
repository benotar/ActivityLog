using System.Text.Json.Serialization;

namespace ActivityLog.Services.WorkoutService.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WorkoutType
{
    Strength = 0,
    Running = 1
}
