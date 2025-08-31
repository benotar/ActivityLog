namespace ActivityLog.SharedKernel.Helpers;

public static class DateTimeHelper
{
    public const string SqlUtcNow = "NOW() AT TIME ZONE 'UTC'";
    
    public static DateTime UtcNow()
        => ToDateTime(DateTimeOffset.Now.UtcDateTime, DateTimeKind.Utc);

    private static DateTime ToDateTime(this DateTime dateTime, DateTimeKind kind) =>
        DateTime.SpecifyKind(dateTime, kind);
}
