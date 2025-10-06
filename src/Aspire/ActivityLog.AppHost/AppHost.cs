using ActivityLog.AppHost.Extensions.Network;
using ActivityLog.AppHost.Extensions.Infrastructure;
using ActivityLog.Constants.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDashboard();

builder.AddDockerComposeEnvironment("env");

var pgUser = builder.AddParameter("pg-user", "admin", true);

var pgPassword = builder.AddParameter("pg-password", "admin", true);

var postgres = builder
    .AddPostgres(Components.Postgres, pgUser, pgPassword, port: Components.Database.Port)
    .WithPgWeb()
    .WithLifetime(ContainerLifetime.Session)
    .WithDataVolume()
    .WithIconName("HomeDatabase");

pgUser.WithParentRelationship(postgres);

var workoutDb = postgres.AddDatabase(Components.Database.Workout);

builder
    .AddProject<Projects.ActivityLog_Services_WorkoutService_API>(Services.Workout)
    .WithExternalHttpEndpoints()
    .WithReference(workoutDb)
    .WaitFor(workoutDb)
    .WithOpenApi()
    .WithHealthCheck();

await builder.Build().RunAsync();
