namespace ActivityLog.Constants.Aspire;

public static class DashboardConstants
{
    public const string ContainerName = "dashboard";
    public const string UiName = "dashboard-ui";
    public const string ImageName = "mcr.microsoft.com/dotnet/nightly/aspire-dashboard:latest";
    public const int UiPort = 18888;
    public const int OtlpPort = 18889;
    public static readonly string Otlp = nameof(Otlp).ToLowerInvariant();
}
