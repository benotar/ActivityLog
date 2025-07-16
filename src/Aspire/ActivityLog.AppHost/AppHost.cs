using ActivityLog.Constants.Aspire;
using ActivityLog.Constants.Services;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("env");

var workout = builder.AddProject<Projects.ActivityLog_Services_WorkoutService_API>(WorkoutConstants.Name)
    .WithHttpEndpoint(name: WorkoutConstants.ApiName, port: WorkoutConstants.HttpPort,
        targetPort: WorkoutConstants.HttpPort)
    .WithHttpHealthCheck("health")
    .WithHttpHealthCheck("alive")
    .WithEnvironment("OTEL_EXPORTER_OTLP_ENDPOINT",
        $"http://{DashboardConstants.ContainerName}:{DashboardConstants.OtlpPort.ToString()}")
    .WithEnvironment("OTEL_SERVICE_NAME", WorkoutConstants.Name)
    .WithEnvironment("OTEL_METRICS_EXPORTER", DashboardConstants.Otlp)
    .WithEnvironment("OTEL_TRACES_EXPORTER", DashboardConstants.Otlp);

builder
    .AddContainer(DashboardConstants.ContainerName, DashboardConstants.ImageName)
    .WithHttpEndpoint(name: DashboardConstants.UiName, port: DashboardConstants.UiPort,
        targetPort: DashboardConstants.UiPort)
    .WithHttpEndpoint(name: DashboardConstants.Otlp, port: DashboardConstants.OtlpPort,
        targetPort: DashboardConstants.OtlpPort)
    .WithEnvironment("DOTNET_DASHBOARD_UNSECURED_ALLOW_ANONYMOUS", "true")
    .WaitFor(workout);

await builder.Build().RunAsync();
