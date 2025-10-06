namespace ActivityLog.Constants.Aspire;

public static class DashboardConstants
{
    public const string Marker = "dashboard";
    public const string ContainerImageName = "mcr.microsoft.com/dotnet/aspire-dashboard:latest";
    public const int ContainerPort = 18888;
    public const int OtlpPort = 18889;
    public const string OtlpProtocol = "otlp";
    public const string GrpcProtocol = "grpc";
    public const string OtelExporterOtlpEndpoint = "OTEL_EXPORTER_OTLP_ENDPOINT";
    public const string OtelExporterOtlpProtocol = "OTEL_EXPORTER_OTLP_PROTOCOL";
    public const string OtelServiceName = "OTEL_SERVICE_NAME";
}
