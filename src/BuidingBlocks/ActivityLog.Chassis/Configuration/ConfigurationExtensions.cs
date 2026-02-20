using ActivityLog.Constants.Aspire;
using Microsoft.Extensions.Configuration;

namespace ActivityLog.Chassis.Configuration;

public static class ConfigurationExtensions
{
    public static string? GetConnectionString(this IConfiguration configuration, string databaseName)
    {
        return configuration.GetSection($"{Components.ConnectionStrings}:{databaseName}").Value;
    }
}
