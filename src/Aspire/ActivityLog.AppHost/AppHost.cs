using ActivityLog.AppHost.Extensions.Network;
using ActivityLog.AppHost.Extensions.Infrastructure;
using ActivityLog.Constants.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDashboard();

builder.AddDockerComposeEnvironment("env");

// Postgres
var postgres = builder.AddConfiguredPostgres();
var workoutDb = postgres.AddDatabase(Components.Postgres.Workout);
var identityDb = postgres.AddDatabase(Components.Postgres.Identity);

// RabbitMQ
var rmq = builder.AddConfiguredRabbitMq();

// Workout service
builder
    .AddProject<Projects.ActivityLog_Services_WorkoutService_API>(Services.Workout)
    .WithExternalHttpEndpoints()
    .WithReference(workoutDb)
    .WithReference(rmq)
    .WaitFor(workoutDb)
    .WaitFor(rmq)
    .WithOpenApi()
    .WithHealthCheck();

builder
    .AddProject<Projects.ActivityLog_IdentityService>(Services.Identity)
    .WithExternalHttpEndpoints()
    .WithReference(identityDb)
    .WaitFor(identityDb)
    .WithOpenApi()
    .WithHealthCheck();

await builder.Build().RunAsync();
