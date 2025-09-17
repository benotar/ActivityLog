namespace ActivityLog.Services.WorkoutService.Application.Options;

public class DatabaseConfiguration
{
    public static readonly string Key = "Database";
    
    public string ConnectionString { get; init; }
    public string Host { get; init; }
    public string User { get; init; }
    public string Password { get; init; }
    public string DbName { get; init; }
}
