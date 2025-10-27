namespace ActivityLog.Services.WorkoutService.Application.Models;

public sealed record WorkoutInfo(
    Guid Id,
    Guid UserId,
    string Notes,
    DateTime CreatedAt,
    DateTime? LastModifiedAt
);

public sealed record CreateWorkoutRequest(
    Guid UserId,
    string Notes
);
