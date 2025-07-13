var builder = DistributedApplication.CreateBuilder(args);

var sharedKernel = builder.AddProject<Projects.ActivityLog_SharedKernel>("shared-kernel");
var chassis = builder.AddProject<Projects.ActivityLog_Chassis>("chassis");

builder.AddProject<Projects.ActivityLog_Services_WorkoutService_API>("workout-service-api")
    .WithReference(sharedKernel)
    .WithReference(chassis);

builder.Build().Run();
