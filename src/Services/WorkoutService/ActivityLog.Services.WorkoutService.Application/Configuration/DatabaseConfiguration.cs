namespace ActivityLog.Services.WorkoutService.Application.Configuration;

public class DatabaseConfiguration
{
    public static readonly string Key = "Database";
    
    public string ConnectionString { get; init; }
    public string User { get; init; }
    public string Password { get; init; }
    public string Name { get; init; }
}
