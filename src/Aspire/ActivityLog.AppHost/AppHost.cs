using ActivityLog.AppHost.Extensions.Network;
using ActivityLog.AppHost.Extensions.Infrastructure;
using ActivityLog.Constants.Database;
using ActivityLog.Constants.Services;
using ActivityLog.Constants.Shared;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDashboard();

builder.AddDockerComposeEnvironment("env");

var postgresUser = builder.AddParameter(PostgresConstants.DbUserParam, secret: true);
var postgresPassword = builder.AddParameter(PostgresConstants.DbPasswordParam, secret: true);
var postgresDbName =
    builder.AddParameter(PostgresConstants.DbNameParam, PostgresConstants.DbName, publishValueAsDefault: true);

var postgresDb = builder.AddPostgres(PostgresConstants.Marker, postgresUser, postgresPassword,
        port: PostgresConstants.Port)
    .WithImage(PostgresConstants.Marker, SharedConstants.LatestTag)
    .WithContainerName(PostgresConstants.ContainerName)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume(PostgresConstants.Volume)
    .WithEnvironment(PostgresConstants.DbNameEnv, postgresDbName);

var workout = builder
    .AddProject<Projects.ActivityLog_Services_WorkoutService_API>(WorkoutConstants.Name)
    .WithHttpEndpoint(name: WorkoutConstants.ApiName, port: WorkoutConstants.HttpPort,
        targetPort: WorkoutConstants.HttpPort)
    .WithReference(postgresDb)
    .WaitFor(postgresDb)
    .WithOpenApi()
    .WithHealthCheck();

    // .WithHttpHealthCheck("health")
    // .WithHttpHealthCheck("alive");
    // .WithEnvironment("OTEL_EXPORTER_OTLP_ENDPOINT",
    //     $"http://{DashboardConstants.Marker}:{DashboardConstants.OtlpPort.ToString()}")
    // .WithEnvironment("OTEL_SERVICE_NAME", WorkoutConstants.Name)
    // .WithEnvironment("OTEL_METRICS_EXPORTER", DashboardConstants.OtlpProtocol)
    // .WithEnvironment("OTEL_TRACES_EXPORTER", DashboardConstants.OtlpProtocol);

// builder
//     .AddContainer(DashboardConstants.Marker, DashboardConstants.ContainerImageName)
//     .WithHttpEndpoint(name: DashboardConstants.UiName, port: DashboardConstants.ContainerPort,
//         targetPort: DashboardConstants.ContainerPort)
//     .WithHttpEndpoint(name: DashboardConstants.OtlpProtocol, port: DashboardConstants.OtlpPort,
//         targetPort: DashboardConstants.OtlpPort)
//     .WithEnvironment("DOTNET_DASHBOARD_UNSECURED_ALLOW_ANONYMOUS", "true");

await builder.Build().RunAsync();
