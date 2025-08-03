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
var postgresDbName = builder
    .AddParameter(PostgresConstants.DbNameParam, PostgresConstants.DbName, publishValueAsDefault: true);

var postgresDb = builder
    .AddPostgres(PostgresConstants.Marker, postgresUser, postgresPassword, port: PostgresConstants.Port)
    .WithImage(PostgresConstants.Marker, SharedConstants.LatestTag)
    .WithContainerName(PostgresConstants.ContainerName)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume(PostgresConstants.Volume)
    .WithEnvironment(PostgresConstants.DbNameEnv, postgresDbName);

var workout = builder
    .AddProject<Projects.ActivityLog_Services_WorkoutService_API>(WorkoutConstants.Name)
    .WithReference(postgresDb)
    .WaitFor(postgresDb)
    .WithOpenApi()
    .WithHealthCheck();

await builder.Build().RunAsync();
